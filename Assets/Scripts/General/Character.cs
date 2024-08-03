using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;//事件


//共同属性，血量，伤害
public class Character : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("基本属性")]
    public int maxHealth;
    public int currentHealth;

    [Header("受伤免疫状态")]
    //无敌时间
    public float duration;
    //计时器
    private float counter;
    //状态值
    public bool able;

    public UnityEvent OnTakeDamage;
    public UnityEvent OnDdie;

    //血条
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // 实例化血条
        GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, transform);
        healthBar = healthBarObject.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.offset = new Vector3(0, 2, 0); // 设置血条的偏移量，使其在头顶
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
    public void TakeDamage(int damage)
    {     
        //判断是否受伤
        //确保人物血量不会出现负值
        if(able)
            return;

        if(currentHealth - damage > 0 && !able)
        {
            currentHealth -= damage;
            TriggerInvulnerable();

            //受伤要执行系列
            //执行ontakedamage上注册的事件-invoke
            OnTakeDamage?.Invoke();
        }
        else
        {
            Debug.Log("怪兽死了");
            currentHealth = 0;
            //播放死亡动画，也可以直接调用死亡函数，不用注册事件
            //仆从比较特殊，要考虑到它的事件
            OnDdie?.Invoke();
            //Died（）
        }
        healthBar.SetHealth(currentHealth);
    }

    //无敌时间的调用
    private void TriggerInvulnerable()
    {
        if (!able)
        {
            able= true;
            counter = duration;
        }
    }

    //击退效果
    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        able = false;
    }
}
