using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public Vector3 positionGo;//������
    public GameSceneSO sceneToGo;//��Ҫת���ĵĳ���
    public SceneLoadEventSO loadsceneEvent;

    public void TriggerAction()
    {
        Debug.Log("����");

        //����/�㲥����
        loadsceneEvent.RaiseLoadRequestEvent(sceneToGo, positionGo, true);
    }
}
