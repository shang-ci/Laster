using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//事件


//共同属性，血量，伤害
public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;

    [Header("受伤免疫状态")]
    //无敌时间
    public float duration;
    //计时器
    private float counter;
    //状态值
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
            counter -= Time.deltaTime;//倒计时
            if(counter <= 0 )
            {
                able = false;
            }
        }
    }

    //接受伤害
    public void TakeDamage(Attack attack)
    {     
        //判断是否受伤
        if (able)
        {
            return;
        }
        //确保人物血量不会出现负值
        if(currentHealth - attack.damage > 0)
        {
            currentHealth -= attack.damage;
            TriggerInvulnerable();
            //受伤要执行系列
            //执行ontakedamage上注册的事件-invoke
            OnTakeDamage?.Invoke(attack.transform);
        }
        else
        {
            currentHealth = 0;
            //触发死亡
            OnDdie?.Invoke();
        }
        
    }

    //无敌时间的调用
    private void TriggerInvulnerable()
    {
        if (!able)
        {
            able = true;
            counter = duration;
        }

    }


}
