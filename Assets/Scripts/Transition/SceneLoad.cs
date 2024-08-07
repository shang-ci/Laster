using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

//�����������ص�����
public class SceneLoad : MonoBehaviour
{
    //���س���

    [Header("�¼�����")]
    public SceneLoadEventSO loadsceneEvent;
    public GameSceneSO firstLoadScene;

    //�洢����
    private GameSceneSO sceneToLoad;
    private Vector3 position;
    private bool fadeScreen;

    public Transform playerTrans;
    private GameSceneSO currentScene;
    public float fadeDuration;//������������ʱ��

    private void Awake()
    {
        //ͨ��address�첽���س�����������غ������������
        //Addressables.LoadSceneAsync(firstLoadScene.sceneRefarence , LoadSceneMode.Additive);Current science �����force load sense�� ֵ���Ǳ����صĻ���false load sense������ ��current senseû�б����� ���������޷���ж�� 
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

    //ȥ���³���
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 position, bool fadeScreen)
    {
        sceneToLoad = locationToLoad;
        this.position = position;
        this.fadeScreen = fadeScreen;

        Debug.Log("���гɹ�");

        StartCoroutine(UnLoadPreviousScene());
    }

    //ж�س���Ҳ��Ҫʱ�䣬��Я��
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //ʵ�ֽ��뽥��
        }

        yield return new WaitForSeconds(fadeDuration);

        //ж��
        if(currentScene != null)
        {
            //ж�����֮���ڼ���
          yield return currentScene.sceneRefarence.UnLoadScene();
            Debug.Log("ж��");
        }

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
        Debug.Log("ת��");
        if(fadeScreen)
        {

        }

    }
}
