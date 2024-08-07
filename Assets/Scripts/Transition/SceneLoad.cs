using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

//监听场景加载的请求
public class SceneLoad : MonoBehaviour
{
    //加载场景

    [Header("事件监听")]
    public SceneLoadEventSO loadsceneEvent;
    public GameSceneSO firstLoadScene;

    //存储变量
    private GameSceneSO sceneToLoad;
    private Vector3 position;
    private bool fadeScreen;

    public Transform playerTrans;
    private GameSceneSO currentScene;
    public float fadeDuration;//渐隐渐现所需时间

    private void Awake()
    {
        //通过address异步加载场景，方便加载后添加其他东西
        //Addressables.LoadSceneAsync(firstLoadScene.sceneRefarence , LoadSceneMode.Additive);Current science 获得了force load sense的 值但是被加载的还是false load sense的引用 而current sense没有被加载 所以他就无法被卸载 
        currentScene = firstLoadScene;
        currentScene.sceneRefarence.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        loadsceneEvent.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        loadsceneEvent.LoadRequestEvent -= OnLoadRequestEvent;
    }

    //去往新场景
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 position, bool fadeScreen)
    {
        sceneToLoad = locationToLoad;
        this.position = position;
        this.fadeScreen = fadeScreen;

        Debug.Log("呼叫成功");

        StartCoroutine(UnLoadPreviousScene());
    }

    //卸载场景也需要时间，用携程
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //实现渐入渐出
        }

        yield return new WaitForSeconds(fadeDuration);

        //卸载
        if(currentScene != null)
        {
            //卸载完成之后在加载
          yield return currentScene.sceneRefarence.UnLoadScene();
            Debug.Log("卸载");
        }

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
        Debug.Log("转移");
        if(fadeScreen)
        {

        }

    }
}
