using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    [Header("广播")]
    public voidEventSO saveDataEvent;

    [Header("变量参数")]
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

            //保存数据
            saveDataEvent.RaiseEvent();

            this.gameObject.tag = "Untagged";
        } 
    }
}
