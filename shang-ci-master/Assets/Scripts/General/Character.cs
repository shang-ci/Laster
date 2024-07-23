using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//�¼�


//��ͬ���ԣ�Ѫ�����˺�
public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float currentHealth;

    [Header("��������״̬")]
    //�޵�ʱ��
    public float duration;
    //��ʱ��
    private float counter;
    //״ֵ̬
    public bool able;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDdie;


    private void Start()
    {
        currentHealth = maxHealth;
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
    public void TakeDamage(Attack attack)
    {     
        //�ж��Ƿ�����
        if (able)
        {
            return;
        }
        //ȷ������Ѫ��������ָ�ֵ
        if(currentHealth - attack.damage > 0)
        {
            currentHealth -= attack.damage;
            TriggerInvulnerable();
            //����Ҫִ��ϵ��
            //ִ��ontakedamage��ע����¼�-invoke
            OnTakeDamage?.Invoke(attack.transform);
        }
        else
        {
            currentHealth = 0;
            //��������
            OnDdie?.Invoke();
        }
        
    }

    //�޵�ʱ��ĵ���
    private void TriggerInvulnerable()
    {
        if (!able)
        {
            able = true;
            counter = duration;
        }

    }


}
