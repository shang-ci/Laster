using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    [Header("�¼�����")]
    public voidEventSO afterSceneLoadEvent;

    private CinemachineConfiner2D confiner;
    public CinemachineImpulseSource impulseSource;//��Դ
    public voidEventSO cameraShakeEvent;//���¼�

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        afterSceneLoadEvent.OnEventRaised += OnAfterSceneLoad;
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnDisable()
    {
        afterSceneLoadEvent.OnEventRaised -= OnAfterSceneLoad;
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }

    private void OnAfterSceneLoad()
    {
        GetNewCameraBouns();
    }

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    //�л����������������
    private void Start()
    {
        GetNewCameraBouns();
    }

    private void GetNewCameraBouns()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");

        if(obj == null)
            return;

        confiner.m_BoundingShape2D = obj.GetComponent<Collider2D>();

        //�������
        confiner.InvalidateCache();
    }
}
