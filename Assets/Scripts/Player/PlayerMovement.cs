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

    //被反弹
    public bool isHurt=false;
    public float hurtForce;
    private bool isDoubleJump; 
    public bool isDead;

    //重生点
    public Transform newPoint;

    //确保切换场景玩家不被销毁
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

        //若处于受伤被弹飞状态，则无法正常移动
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

    // 控制角色跳跃的方法
    void Jump()
    {
        if (Input.GetButtonDown("Jump")) // 如果按下跳跃按钮
        {
            if (physicsCheck.isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 施加跳跃力
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
        //获得被弹开的方向
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0);

        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        //让人物不能被控制
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetBool("isDead",isDead);
        //SceneManager.LoadScene("GameScenes");    
    }

    //下面全是为了重生

    public void RestartGame ()
    {
        Debug.Log("切换");
        Debug.Log(newPoint.transform);
        transform.position = newPoint.position;
        GetComponent<Character>().currentHealth = 4000;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isDead = false;
        SceneManager.LoadScene("GameScenes");
    }

    //注册场景加载事件，确保切换场景时，玩家重生点不会消失
    private void OnEnable()
    {
        SceneManager.sceneLoaded += Loaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Loaded;
    }

    // 只要加载场景就寻找重生点
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
            Debug.Log("找不到带有 'RespawnPoint' 标签的对象");
        }
    }

}
