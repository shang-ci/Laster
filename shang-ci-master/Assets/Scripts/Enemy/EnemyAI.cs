using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    [Header("��������")]
    public float normalSpeed;
    //׷���ٶ�
    public float chaseSpeed;
    public float currentSpeed;
    //�Ѳ鷶Χ
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
            Vector2 direction = (player.position - transform.position).normalized; // ���㳯����ҵķ���
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y); // �������˵��ٶ��Գ������
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // ��Ҳ��ڼ�ⷶΧ��ʱ������ֹͣ�ƶ�
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
