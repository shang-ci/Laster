using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerState playerState;

    [Header("事件监听")]
    public CharaterEventSo heathEvent;

    private void OnEnable()
    {
        heathEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        heathEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persent = character.currentHealth / character.maxHealth;
        playerState.OnHealthChange(persent);
        Debug.Log("更新血条");
    }
}
