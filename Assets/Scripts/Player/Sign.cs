using UnityEngine;

public class Sign : MonoBehaviour
{
    private Animator anim;
    public GameObject signSprite;
    public Transform playerTrans;
    private bool canPress;
    private IInteractable targetItem;

    public AudioSource chestaudio;

    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
    }

    private void Update()
    {
        if(canPress && Input.GetKeyDown(KeyCode.E))
        {
            targetItem.TriggerAction();
            chestaudio.Play();
        }

        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTrans.localScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("interaction"))
        {
            canPress = true;
            targetItem = collision.GetComponent<IInteractable>();
            anim.Play("chest");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }
}
