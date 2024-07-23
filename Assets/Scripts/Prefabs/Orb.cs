using UnityEngine;

public class Orb : MonoBehaviour
{
    public int damage = 30;
    public float lifetime = 60f;
    public float orbitSpeed = 50f; // ��ת�ٶ�
    public float orbitDistance = 2f; // ��ת�뾶

    private Transform bossTransform;
    private float angle;

    void Start()
    {
        //60s������
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Transform boss)
    {
        bossTransform = boss;
        angle = Random.Range(0f, 360f); // �����ʼ�Ƕ�
    }

    public void Update()
    {
        //�÷�����ת
        if (bossTransform != null)
        {
            // ���㵱ǰ�Ƕ�
            angle -= orbitSpeed * Time.deltaTime; // ˳ʱ����ת
            if (angle <= -360f)
                angle += 360f;

            // �����µ�λ��
            float rad = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * orbitDistance;
            transform.position = bossTransform.position + offset;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().TakeDamage(damage);
        }
    }
}
