using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;//�¼�


//��ͬ���ԣ�Ѫ�����˺�
public class Character : MonoBehaviour, ISacvAble
{
    Rigidbody2D rb;

    [Header("�¼�����")]
    public voidEventSO newGameEvent;

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

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDdie;

    //Ѫ��
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    private void NewGame()
    {
        //OnHealthChange?.Invoke(this);

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // ʵ����Ѫ��
        //GameObject healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, transform);
        //healthBar = healthBarObject.GetComponent<HealthBar>();
        //healthBar.SetMaxHealth(maxHealth);
        //healthBar.offset = new Vector3(0, 2, 0); // ����Ѫ����ƫ������ʹ����ͷ��
    }

    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;

        //��Ϊ�ڽӿ���ʵ���˾�����Ϊ���ڼ̳�������ֱ��ǿ�е���
        ISacvAble saveable = this;
        saveable.RegisterSaveData();//ע��
    }

    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;

        ISacvAble sacvAble = this;
        sacvAble.UnRegisterSaveData();//ע��
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
            rb.velocity = new Vector2 (0, rb.velocity.y);
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
        //healthBar.SetHealth(currentHealth);

        OnHealthChange?.Invoke(this);
        Debug.Log("��Ѫ");
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

    //��ñ�ʶ
    public DataDefinition GetDataID()
    {
        //�����ϵ�DataDefinition��������
        return GetComponent<DataDefinition>();
    }

    //���棬��data����
    public void GetSaveData(Data data)
    {
        //����ӦID����ص�Ҫ�����ֵʱ���ڽ��б���㱣��ͻ���µ�ֵ���¸��貢����
        if (data.characterPosdict.ContainsKey(GetDataID().ID))
        {
            data.characterPosdict[GetDataID().ID] = transform.position;
            data.floatSaveData[GetDataID().ID + "health"] = this.currentHealth;
        }
        else 
        { 
            //���ǵ�һ�α��棬�ͻ�������һ��
            data.characterPosdict.Add(GetDataID().ID, transform.position);
            data.floatSaveData.Add(GetDataID().ID + "health", currentHealth);
        }
    }

    //��������
    public void LoadData(Data data)
    {
        //�ҵ�character��Ӧ��id������ص�data����
        if (data.characterPosdict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosdict[GetDataID().ID];
            this.currentHealth = data.floatSaveData[GetDataID().ID + "health"];

            OnHealthChange?.Invoke(this);
        }
    }
}
