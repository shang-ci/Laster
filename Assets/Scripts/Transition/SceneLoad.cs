using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

//监听场景加载的请求
public class SceneLoad : MonoBehaviour, ISacvAble
{
    //加载场景

    [Header("事件监听")]
    public SceneLoadEventSO loadsceneEvent;
    public voidEventSO NewGameEvent;
    public voidEventSO backToMenuEvent;

    [Header("广播")]
    public voidEventSO afterSceneLoad;
    public FadeEventSO fadeEvent;


    [Header("场景")]
    //存储变量
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene; 
    private GameSceneSO sceneToLoad;
    private Vector3 position;
    private bool fadeScreen;

    private bool isLoading;
    public Transform playerTrans;
    public Vector3 firstPosition;
    public Vector3 menuPosition;
    private GameSceneSO currentScene;
    public float fadeDuration;//渐隐渐现所需时间

    private void Awake()
    {
        //通过address异步加载场景，方便加载后添加其他东西
        //Addressables.LoadSceneAsync(firstLoadScene.sceneRefarence , LoadSceneMode.Additive);Current science 获得了force load sense的 值但是被加载的还是false load sense的引用 而current sense没有被加载 所以他就无法被卸载 

        //currentScene = firstLoadScene;
        //currentScene.sceneRefarence.LoadSceneAsync(LoadSceneMode.Additive);

        
    }

    private void Start()
    {
        loadsceneEvent.RaiseLoadRequestEvent(menuScene, menuPosition, true);
        //NewGame();
    }

    private void OnEnable()
    {
        NewGameEvent.OnEventRaised += NewGame;
        loadsceneEvent.LoadRequestEvent += OnLoadRequestEvent;
        backToMenuEvent.OnEventRaised += OnBackToMenu;

        ISacvAble saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        NewGameEvent.OnEventRaised -= NewGame;
        loadsceneEvent.LoadRequestEvent -= OnLoadRequestEvent;
        backToMenuEvent.OnEventRaised -= OnBackToMenu;

        ISacvAble saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void OnBackToMenu()
    {
        sceneToLoad = menuScene;
        loadsceneEvent.RaiseLoadRequestEvent(sceneToLoad, menuPosition, true);
    }

    private void NewGame()
    {
        sceneToLoad = firstLoadScene;
        //OnLoadRequestEvent(sceneToLoad, firstPosition, true);
        //上面这个代码没有通过事件而是直接调用这个函数来加载场景 所以导致加载第一个场景的时候没有执行把玩家状态以及控制玩家能否移动的系统 的代码给激活 
        loadsceneEvent.RaiseLoadRequestEvent(sceneToLoad, firstPosition, true);
    }

    //去往新场景
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 position, bool fadeScreen)
    {
        //防止加载两个场景时一直按着e不断来回在两个场景切换
        if(isLoading)
            return;

        isLoading = true;
        sceneToLoad = locationToLoad;
        this.position = position;
        this.fadeScreen = fadeScreen;

        Debug.Log("呼叫成功");

        if(currentScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            LoadNewScene();
        }
    }

    //卸载场景也需要时间，用携程
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //实现渐入渐出
            fadeEvent.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);

        //卸载
        if(currentScene != null)
        {
            //卸载完成之后在加载
          yield return currentScene.sceneRefarence.UnLoadScene();
            Debug.Log("卸载");
        }

        playerTrans.gameObject.SetActive(false);

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        Debug.Log("加载");
       var loadingOption = sceneToLoad.sceneRefarence.LoadSceneAsync(LoadSceneMode.Additive, true);
       
        //加载完要执行的
        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentScene = sceneToLoad;

        playerTrans.position = position;
        playerTrans.gameObject.SetActive(true);

        Debug.Log("转移");
        if(fadeScreen)
        {
            //变透明
            fadeEvent.FadeOut(fadeDuration);
        }

        isLoading = false;

        //让相机获得边界
        afterSceneLoad.RaiseEvent();
    }

    public DataDefinition GetDataID()
    {
        return GetComponent<DataDefinition>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveGameScene(currentScene);
    }

    public void LoadData(Data data)
    {
        var playerID = playerTrans.GetComponent<DataDefinition>().ID;
        if (data.characterPosdict.ContainsKey(playerID))
        {
            sceneToLoad = data.GetSavedScene();
            //获得保存的位置
            position = data.characterPosdict[playerID];

            OnLoadRequestEvent(sceneToLoad, position, true);
        }
    }
}
