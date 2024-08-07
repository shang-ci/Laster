using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "CharaterEventSo")]
public class CharaterEventSo : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;

    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}
