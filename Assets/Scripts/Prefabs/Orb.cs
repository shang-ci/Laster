using UnityEngine;

public class Orb : MonoBehaviour
{
    public int damage = 30;
    public float lifetime = 60f;
    public float orbitSpeed = 50f; // 旋转速度
    public float orbitDistance = 2f; // 旋转半径

    private Transform bossTransform;
    private float angle;

    void Start()
    {
        //60s后销毁
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Transform boss)
    {
        bossTransform = boss;
        angle = Random.Range(0f, 360f); // 随机初始角度
    }

    public void Update()
    {
        //让法球旋转
        if (bossTransform != null)
        {
            // 计算当前角度
            angle -= orbitSpeed * Time.deltaTime; // 顺时针旋转
            if (angle <= -360f)
                angle += 360f;

            // 计算新的位置
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
