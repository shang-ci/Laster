using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;//�¼�


//��ͬ���ԣ�Ѫ�����˺�
public class Character : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("��������")]
    public int maxHealth;
    public int currentHealth;

    [Header("��������״̬")]
    //�޵�ʱ��
    public float duration;
    //��ʱ��
    private float counter;
    //״ֵ̬
    public bool able;

    public UnityEvent OnTakeDamage;
    public UnityEvent OnDdie;

    //Ѫ��
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // ʵ����Ѫ��
        GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, transform);
        healthBar = healthBarObject.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.offset = new Vector3(0, 2, 0); // ����Ѫ����ƫ������ʹ����ͷ��
    }

    private void Update()
    {
        if(able)
        {
            counter -= Time.deltaTime;//����ʱ
            if(counter <= 0 )
            {
                able = false;
            }
        }
    }

    //�����˺�
    public void TakeDamage(int damage)
    {     
        //�ж��Ƿ�����
        //ȷ������Ѫ��������ָ�ֵ
        if(able)
            return;

        if(currentHealth - damage > 0 && !able)
        {
            currentHealth -= damage;
            TriggerInvulnerable();

            //����Ҫִ��ϵ��
            //ִ��ontakedamage��ע����¼�-invoke
            OnTakeDamage?.Invoke();
        }
        else
        {
            Debug.Log("��������");
            currentHealth = 0;
            //��������������Ҳ����ֱ�ӵ�����������������ע���¼�
            //�ʹӱȽ����⣬Ҫ���ǵ������¼�
            OnDdie?.Invoke();
            //Died����
        }
        healthBar.SetHealth(currentHealth);
    }

    //�޵�ʱ��ĵ���
    private void TriggerInvulnerable()
    {
        if (!able)
        {
            able= true;
            counter = duration;
        }
    }

    //����Ч��
    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        able = false;
    }
}
