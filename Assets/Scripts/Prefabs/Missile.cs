using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform target; 
    public float speed = 5f; // ���������ٶ�
    private Rigidbody2D rb;
    public float time;//����ʱ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // ׷��Ŀ��
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void FixedUpdate()
    {
        //׷�����
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
            Debug.Log("��������");
            collision.gameObject.GetComponent<Character>().TakeDamage(10); // ���������˺�
            Destroy(gameObject); 
        }
    }
}
