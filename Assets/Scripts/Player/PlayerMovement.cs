using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("�¼�����")]
    public voidEventSO loadDataEvent;
    public voidEventSO backToMenuEvent;

    private Animator animator;
    public float jumpForce;
    public float runSpeed; 
    public float maxSpeed = 20f;
    public float wallJumpForce;
    private Rigidbody2D rb; 
    private PlayerAnimation playerAnimation;
    private SpriteRenderer spriteRenderer;
    private PhysicsCheck physicsCheck;
    private float move;
    private static PlayerMovement instance;

    //������
    public bool isHurt=false;
    public float hurtForce;
    private bool isDoubleJump; 
    public bool isDead;

    //������
    public Transform newPoint;

    //ȷ���л�������Ҳ�������
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        loadDataEvent.OnEventRaised += OnloadDataEvent;
        backToMenuEvent.OnEventRaised += OnloadDataEvent;
    }

    private void OnDisable()
    {
        loadDataEvent.OnEventRaised -= OnloadDataEvent;
        backToMenuEvent.OnEventRaised += OnloadDataEvent;
    }

    //��ȡ��Ϸ����
    private void OnloadDataEvent()
    {
        isDead = false;
    }

    void Start()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        playerAnimation = GetComponent<PlayerAnimation>(); 
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal"); 

        //���������˱�����״̬�����޷������ƶ�
        if(!isHurt && !isDead ) 
        {
            Move(); 
            Jump(); 
        }

        if(physicsCheck.onWall)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2f); 
        else
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }

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

    // ���ƽ�ɫ��Ծ�ķ���
    void Jump()
    {
        if (Input.GetButtonDown("Jump")) // ���������Ծ��ť
        {
            if (physicsCheck.isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ʩ����Ծ��
                isDoubleJump = true;
            }
            else if (isDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isDoubleJump = false;
            }
            else if (physicsCheck.onWall)
            {
                rb.AddForce(new Vector2(-move, 2f) * wallJumpForce, ForceMode2D.Impulse);
            }

        }

    }

    public void GetHurt(Transform attacker)
    {
        Debug.Log("hurt");
        isHurt = true;
        rb.velocity = Vector2.zero;
        //��ñ������ķ���
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0);

        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        //�����ﲻ�ܱ�����
        isDead = true;
       // rb.bodyType = RigidbodyType2D.Static;
        animator.SetBool("isDead",isDead);
        //SceneManager.LoadScene("GameScenes");    
    }

    //����ȫ��Ϊ������

   // public void RestartGame ()
   // {
   //     Debug.Log("�л�");
   //     Debug.Log(newPoint.transform);
   //     transform.position = newPoint.position;
   //     GetComponent<Character>().currentHealth = 4000;
   //     rb.bodyType = RigidbodyType2D.Dynamic;
   //     isDead = false;
   //     SceneManager.LoadScene("GameScenes");
   // }
}
