using UnityEngine;

public class BoosMove : MonoBehaviour
{
    public float Range;
    public float MoveSpeed;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }
    private void DetectPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform. position);

        // 如果玩家在追踪范围内，怪物向玩家移动
        if (distanceToPlayer < Range)
        {
            // 计算怪物向玩家的方向
            Vector2 direction = (player.transform. position - transform.position).normalized;

            // 移动怪物
            rb.velocity = new Vector2(direction.x * MoveSpeed, rb.velocity.y);

            // 让怪物朝向玩家
            Vector3 scale = transform.localScale;
            if (direction.x > 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else if (direction.x < 0)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}

