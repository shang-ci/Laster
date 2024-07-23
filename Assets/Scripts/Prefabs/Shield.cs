using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldValue; // ����ֵ
    public int damage = 30;

    //����Boos
    private void Update()
    {
        if (transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }

    public void SetShieldValue(float value)
    {
        shieldValue = value;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Character>().TakeDamage(damage);
            shieldValue -= 50f;//��ȥ��ҵĹ�����

            if (shieldValue <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

