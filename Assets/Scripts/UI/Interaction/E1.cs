using UnityEngine;

public class E1 : MonoBehaviour
{
    private Animator animator;
    //private Animation animation;
    private bool canPress;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //animation = GetComponent<Animation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
            spriteRenderer.enabled = canPress;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            canPress = true;
            animator.Play("chest");
            Debug.Log("²¥·Å");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
        //animation.Stop("chest");
    }
}
