using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    [Header("��������")]
    public float normalSpeed;
    //׷���ٶ�
    public float chaseSpeed;
    public float currentSpeed;
    //�Ѳ鷶Χ
    public float range = 10f;

    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Chase();
    }

    public void Chase()
    {
        if (Vector2.Distance(transform.position, player.position) < range)
        {
            Vector2 direction = (player.position - transform.position).normalized; 
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

            if (direction.x >= 0f)
            {
                spriteRenderer.flipX = false; // ������
            }
            else if (direction.x < 0f)
            {
                spriteRenderer.flipX = true; // ������
            }
        }
        else
        {
            rb.velocity = Vector2.zero; //ֹͣ�ƶ�
        }
    }

    public void Dies()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
