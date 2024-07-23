using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator; // Animator组件的引用
    private PhysicsCheck physicsCheck;
    private PlayerMovement playerMovement;

    public AudioSource JSource;
    public AudioSource KSource;
    public AudioSource LSource;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // 获取Animator组件
    }

    private void Update()
    {
        SetAnimation(); 
    }

    public void SetAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isGround",physicsCheck.isGround);
        animator.SetBool("isDead", playerMovement.isDead);
        if (Input.GetKeyDown(KeyCode.J))
        {
            JSource.Play();
            animator.SetTrigger("Atk");
        }
            
        if (Input.GetKeyDown(KeyCode.K))
        {
            KSource.Play();
            animator.SetTrigger("RedSword");
        }
            
        if (Input.GetKeyDown(KeyCode.L))
        {
            LSource.Play();
            animator.SetTrigger("Strike");
        }
            
    }

    public void PlayerHurt()
    {
        animator.SetTrigger("Hurt");
    }


    

}
