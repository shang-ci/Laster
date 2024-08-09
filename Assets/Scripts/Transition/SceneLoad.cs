using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

//�����������ص�����
public class SceneLoad : MonoBehaviour, ISacvAble
{
    //���س���

    [Header("�¼�����")]
    public SceneLoadEventSO loadsceneEvent;
    public voidEventSO NewGameEvent;
    public voidEventSO backToMenuEvent;

    [Header("�㲥")]
    public voidEventSO afterSceneLoad;
    public FadeEventSO fadeEvent;


    [Header("����")]
    //�洢����
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
    public float fadeDuration;//������������ʱ��

    private void Awake()
    {
        //ͨ��address�첽���س�����������غ������������
        //Addressables.LoadSceneAsync(firstLoadScene.sceneRefarence , LoadSceneMode.Additive);Current science �����force load sense�� ֵ���Ǳ����صĻ���false load sense������ ��current senseû�б����� ���������޷���ж�� 

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
        //�����������û��ͨ���¼�����ֱ�ӵ���������������س��� ���Ե��¼��ص�һ��������ʱ��û��ִ�а����״̬�Լ���������ܷ��ƶ���ϵͳ �Ĵ�������� 
        loadsceneEvent.RaiseLoadRequestEvent(sceneToLoad, firstPosition, true);
    }

    //ȥ���³���
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 position, bool fadeScreen)
    {
        //��ֹ������������ʱһֱ����e�������������������л�
        if(isLoading)
            return;

        isLoading = true;
        sceneToLoad = locationToLoad;
        this.position = position;
        this.fadeScreen = fadeScreen;

        Debug.Log("���гɹ�");

        if(currentScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            LoadNewScene();
        }
    }

    //ж�س���Ҳ��Ҫʱ�䣬��Я��
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //ʵ�ֽ��뽥��
            fadeEvent.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);

        //ж��
        if(currentScene != null)
        {
            //ж�����֮���ڼ���
          yield return currentScene.sceneRefarence.UnLoadScene();
            Debug.Log("ж��");
        }

        playerTrans.gameObject.SetActive(false);

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        Debug.Log("����");
       var loadingOption = sceneToLoad.sceneRefarence.LoadSceneAsync(LoadSceneMode.Additive, true);
       
        //������Ҫִ�е�
        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentScene = sceneToLoad;

        playerTrans.position = position;
        playerTrans.gameObject.SetActive(true);

        Debug.Log("ת��");
        if(fadeScreen)
        {
            //��͸��
            fadeEvent.FadeOut(fadeDuration);
        }

        isLoading = false;

        //�������ñ߽�
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
            //��ñ����λ��
            position = data.characterPosdict[playerID];

            OnLoadRequestEvent(sceneToLoad, position, true);
        }
    }
}
