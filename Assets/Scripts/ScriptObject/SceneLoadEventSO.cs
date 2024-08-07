using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

    //bool表示是否要渐入渐出场景的效果
    public void RaiseLoadRequestEvent(GameSceneSO locationLoad, Vector3 postion ,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationLoad, postion, fadeScreen);
    }
}
