using System;
using UnityEngine;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    public PlayerState playerState;

    [Header("�¼�����")]
    public CharaterEventSo heathEvent;
    public SceneLoadEventSO unloadedSceneEvent;//ж�س���,�����ֵ�����д�
    public voidEventSO loadDataEvent;//������Ϸ,
    public voidEventSO gameOverEvent;//�������ڱ���㸴��
    public voidEventSO backToMenuEvent;

    [Header("���")]
    public GameObject gameOverPanel;
    public GameObject restarBtn;

    private void OnEnable()
    {
        heathEvent.OnEventRaised += OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent += OnLoadEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        backToMenuEvent.OnEventRaised += OnLoadDataEvent;
    }

    private void OnDisable()
    {
        heathEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnLoadEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvent;
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnLoadDataEvent()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnLoadEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = sceneToLoad.scenesType == ScenesType.Menu;
        playerState.gameObject.SetActive(!isMenu);
    }

    private void OnHealthEvent(Character character)
    {
        var persent = character.currentHealth / character.maxHealth;
        playerState.OnHealthChange(persent);
        Debug.Log("����Ѫ��");
    }
}
