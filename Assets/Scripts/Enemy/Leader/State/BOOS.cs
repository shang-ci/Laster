using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Skill[] phase1Skills; // ��һ�׶εļ�������
    public Skill[] phase2Skills; // �ڶ�
    private int currentPhase = 1; // ��¼�׶�
    public float skillInterval;//ȫ�ּ����ͷż����ȷ������ͬʱ�ͷŶ������
    private float nextSkillTime = 0f;
    private float currentHealth;
    private float maxHealth;
    private Transform postion;

    private void Start()
    {
        maxHealth = GetComponent<Character>().maxHealth;
    }

    public void Update()
    {
        //�û�������Boos��
        //postion.position = transform.position;

        currentHealth = GetComponent<Character>().currentHealth;
        if (currentHealth <= maxHealth / 2 && currentPhase == 1)
        {
            Phase2();
        }

        //�������м����ͷŵ��ж�
        if (Time.time >= nextSkillTime)
        {
            Debug.Log(this.GetType().Name );
            WaitSkill();
            nextSkillTime = Time.time + skillInterval;
        }
    }

    public void Phase2()
    {
        currentPhase = 2;
         
        //��������,����ǿ�������ܼ������
        //maxHealth *= 2;
        currentHealth = maxHealth;
        skillInterval -= 5f;
        Debug.Log("�ڶ��׶�");
    }

    //�ֶ��ͷż��ܣ����п���
    public void UseSkill(int t)
    {
        if (currentPhase == 1)
        {
            phase1Skills[t].UseSkill();
        }
        else
        {
            phase2Skills[t].UseSkill();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Boss died");
        SceneManager.LoadScene("EndScenes");
    }

    //��������ȴ�Լ��ͷż���������ͷż���
    private void WaitSkill()
    {
        Skill[] currentSkills = currentPhase == 1 ? phase1Skills : phase2Skills;

        foreach (Skill skill in currentSkills)
        {
            if (skill.CanUseSkill() )
            {
                Debug.Log(skill.name);
                skill.TryUseSkill();
                break; // ȷ�����ܲ�ͬʱ�ͷ�
            }
        }
    }
}

