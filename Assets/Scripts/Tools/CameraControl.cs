using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    [Header("事件监听")]
    public voidEventSO afterSceneLoadEvent;

    private CinemachineConfiner2D confiner;
    public CinemachineImpulseSource impulseSource;//振动源
    public voidEventSO cameraShakeEvent;//振动事件

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

    //切换场景更改相机限制
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

        //清除缓存
        confiner.InvalidateCache();
    }
}
