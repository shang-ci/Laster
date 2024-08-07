using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Rigidbody2D rb;
    public Animator animator;
    protected SpriteRenderer spriteRenderer;
    public  PhysicsCheck physicsCheck;
    [Header("基本参数")]
    public float normalSpeed;
    //追击速度
    public float chaseSpeed;
    public float currentSpeed;
    //搜查范围
    public float range = 10f;

    public Vector3 fixDir;
    private Transform player;

    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;

    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask checkLayer;

    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //waitTimeCounter = waitTime;
    }


    private void OnEnable()
    {
        Debug.Log("state");
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        fixDir = new Vector3(transform.localScale.x, 0, 0);

        currentState.LogicUpdate();
        TimeCounter();
        
    }

    private void FixedUpdate()
    {
        if(!wait)
        {
            Move();
        }
        
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * fixDir.x * Time.deltaTime, rb.velocity.y);
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, fixDir, checkDistance, checkLayer);
    }

    public void SwichState(EnemiesState state)
    {
        var newState = state switch
        {
            EnemiesState.Patrol => patrolState,
            EnemiesState.Chase => chaseState,
            _ => null,
        };

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public void Dies()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * fixDir.x , 0 , 0), 0.2f);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(-fixDir.x, 1, 1);
            }
        }

        if (!FoundPlayer())
        {
            lostTimeCounter -= Time.deltaTime;
        }
        else
        {
            lostTimeCounter = lostTime;
        }
    }
}
