using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldValue; // 护盾值
    public int damage = 30;

    //跟随Boos
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
            shieldValue -= 50f;//减去玩家的攻击力

            if (shieldValue <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

