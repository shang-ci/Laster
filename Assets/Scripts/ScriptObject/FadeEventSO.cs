using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float, bool> OnEventRaised;

    //让画布变黑
    public void FadeIn(float duration)
    {
        RaiseEvent(Color.black, duration, true);
    }

    //让画布变透明
    public void FadeOut(float duration) 
    {
        RaiseEvent(Color.clear, duration, false);
    }

    public void RaiseEvent(Color target, float duration, bool fadeIn)
    {
        OnEventRaised?.Invoke(target, duration, fadeIn);
    }
}
