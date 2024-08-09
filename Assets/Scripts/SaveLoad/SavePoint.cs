using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    [Header("�㲥")]
    public voidEventSO saveDataEvent;

    [Header("��������")]
    public SpriteRenderer spriteRenderer;
    public Sprite darkSprite;
    public Sprite lightSprite;
    public bool isSave;


    private void OnEnable()
    {
        spriteRenderer.sprite = isSave ? lightSprite : darkSprite;
    }

    public void TriggerAction()
    {
       if(!isSave)
        {
            isSave = true;
            spriteRenderer.sprite = lightSprite;

            //��������
            saveDataEvent.RaiseEvent();

            this.gameObject.tag = "Untagged";
        } 
    }
}
