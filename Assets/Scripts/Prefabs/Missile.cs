using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform target; 
    public float speed = 5f; // 导弹飞行速度
    private Rigidbody2D rb;
    public float time;//存在时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 追踪目标
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void FixedUpdate()
    {
        //追着玩家
        if (target != null && time >= 0)
        {
            Vector2 direction = (target.position - transform.position).normalized; 
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime); 
            time -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("导弹射中");
            collision.gameObject.GetComponent<Character>().TakeDamage(10); // 对玩家造成伤害
            Destroy(gameObject); 
        }
    }
}
