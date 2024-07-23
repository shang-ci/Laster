using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    public float jumpForce;
    public float runSpeed; 
    public float maxSpeed = 20f;
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
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetBool("isDead",isDead);
        //SceneManager.LoadScene("GameScenes");    
    }

    //����ȫ��Ϊ������

    public void RestartGame ()
    {
        Debug.Log("�л�");
        Debug.Log(newPoint.transform);
        transform.position = newPoint.position;
        GetComponent<Character>().currentHealth = 4000;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isDead = false;
        SceneManager.LoadScene("GameScenes");
    }

    //ע�᳡�������¼���ȷ���л�����ʱ����������㲻����ʧ
    private void OnEnable()
    {
        SceneManager.sceneLoaded += Loaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Loaded;
    }

    // ֻҪ���س�����Ѱ��������
    private void Loaded(Scene scene, LoadSceneMode mode)
    {
        FindRespawnPoint(); 
    }

    public void FindRespawnPoint()
    {
        GameObject respawnPoint = GameObject.FindWithTag("RespawnPoint");
        if (respawnPoint != null)
        {
            newPoint = respawnPoint.transform;
            Debug.Log(newPoint.transform);
        }
        else
        {
            Debug.Log("�Ҳ������� 'RespawnPoint' ��ǩ�Ķ���");
        }
    }

}
