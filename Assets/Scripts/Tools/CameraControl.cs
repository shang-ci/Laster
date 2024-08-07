using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    public CinemachineImpulseSource impulseSource;//��Դ
    public voidEventSO cameraShakeEvent;//���¼�

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
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
