using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 6f;
    public float runSpeed; // 角色奔跑速度
    private Rigidbody2D rb; // Rigidbody2D组件的引用
    private PlayerAnimation playerAnimation; // PlayerAnimation组件的引用
    private SpriteRenderer spriteRenderer;
    private PhysicsCheck physicsCheck;
    private float move;

    //被反弹
    public bool isHurt=false;
    public float hurtForce;

    public bool isDead;


    void Start()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // 获取Rigidbody2D组件
        playerAnimation = GetComponent<PlayerAnimation>(); // 获取PlayerAnimation组件
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal"); // 获取水平输入

        //若处于受伤被弹飞状态，则无法正常移动
        if(!isHurt && !isDead ) 
        {
            Move(); 
            Jump(); 
        }

       //Attack(); // 调用Attack方法控制角色攻击
    }

    // 控制角色移动的方法
    void Move()
    {

        rb.velocity = new Vector2(move * runSpeed , rb.velocity.y);
        if (move > 0f)
        {
            spriteRenderer.flipX = false;
        }
        if(move < 0f)
        {
            spriteRenderer.flipX=true;
        }

    }

    // 控制角色跳跃的方法
    void Jump()
    {
        if (Input.GetButtonDown("Jump")) // 如果按下跳跃按钮
        {
            if(physicsCheck.isGround)
            rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse); // 给角色添加向上的瞬时力

        }
    }

    public void GetHurt(Transform attacker)
    {
        Debug.Log("hurt");
        isHurt = true;
        rb.velocity = Vector2.zero;
        //获得被弹开的方向
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0);

        rb.AddForce(dir * hurtForce);
    }

    public void PlayerDead()
    {
        isDead = true;
        //让人物无法操作
        rb.bodyType = RigidbodyType2D.Static;
    }

}
