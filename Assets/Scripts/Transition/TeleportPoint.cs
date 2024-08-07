using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public Vector3 positionGo;//重生点
    public GameSceneSO sceneToGo;//将要转换的的场景
    public SceneLoadEventSO loadsceneEvent;

    public void TriggerAction()
    {
        Debug.Log("传送");

        //呼叫/广播请求
        loadsceneEvent.RaiseLoadRequestEvent(sceneToGo, positionGo, true);
    }
}
