using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;//事件


//共同属性，血量，伤害
public class Character : MonoBehaviour, ISacvAble
{
    Rigidbody2D rb;

    [Header("事件监听")]
    public voidEventSO newGameEvent;

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

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDdie;

    //血条
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    private void NewGame()
    {
        //OnHealthChange?.Invoke(this);

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // 实例化血条
        //GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, transform);
        //healthBar = healthBarObject.GetComponent<HealthBar>();
        //healthBar.SetMaxHealth(maxHealth);
        //healthBar.offset = new Vector3(0, 2, 0); // 设置血条的偏移量，使其在头顶
    }

    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;

        //因为在接口上实现了具体行为，在继承者身上直接强行调用
        ISacvAble saveable = this;
        saveable.RegisterSaveData();//注册
    }

    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;

        ISacvAble sacvAble = this;
        sacvAble.UnRegisterSaveData();//注销
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
            rb.velocity = new Vector2 (0, rb.velocity.y);
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
        //healthBar.SetHealth(currentHealth);

        OnHealthChange?.Invoke(this);
        Debug.Log("掉血");
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

    //获得标识
    public DataDefinition GetDataID()
    {
        //把身上的DataDefinition传过来了
        return GetComponent<DataDefinition>();
    }

    //保存，把data保存
    public void GetSaveData(Data data)
    {
        //当对应ID有相关的要保存的值时，在进行保存点保存就会把新的值重新赋予并保存
        if (data.characterPosdict.ContainsKey(GetDataID().ID))
        {
            data.characterPosdict[GetDataID().ID] = transform.position;
            data.floatSaveData[GetDataID().ID + "health"] = this.currentHealth;
        }
        else 
        { 
            //若是第一次保存，就会新生成一个
            data.characterPosdict.Add(GetDataID().ID, transform.position);
            data.floatSaveData.Add(GetDataID().ID + "health", currentHealth);
        }
    }

    //加载数据
    public void LoadData(Data data)
    {
        //找到character对应的id，把相关的data加载
        if (data.characterPosdict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosdict[GetDataID().ID];
            this.currentHealth = data.floatSaveData[GetDataID().ID + "health"];

            OnHealthChange?.Invoke(this);
        }
    }
}
