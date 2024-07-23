using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    [Header("基本参数")]
    public float normalSpeed;
    //追击速度
    public float chaseSpeed;
    public float currentSpeed;
    //搜查范围
    public float range = 10f;

    private Transform player;
    //public Vector2 faceDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //faceDir = new Vector2(transform.localScale.x,0);
        Chase();
    }

    private void FixedUpdate()
    {
        //Move();
    }

   // public void Move()
   // {
   //     rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
   // }

    public void Chase()
    {
        if (Vector2.Distance(transform.position, player.position) < range)
        {
            Vector2 direction = (player.position - transform.position).normalized; // 计算朝向玩家的方向
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y); // 调整敌人的速度以朝向玩家
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // 玩家不在检测范围内时，敌人停止移动
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
