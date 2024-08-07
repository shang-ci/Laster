using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

    //bool��ʾ�Ƿ�Ҫ���뽥��������Ч��
    public void RaiseLoadRequestEvent(GameSceneSO locationLoad, Vector3 postion ,bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationLoad, postion, fadeScreen);
    }
}
