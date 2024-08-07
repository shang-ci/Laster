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

        // ��������׷�ٷ�Χ�ڣ�����������ƶ�
        if (distanceToPlayer < Range)
        {
            // �����������ҵķ���
            Vector2 direction = (player.transform. position - transform.position).normalized;

            // �ƶ�����
            rb.velocity = new Vector2(direction.x * MoveSpeed, rb.velocity.y);

            // �ù��ﳯ�����
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

