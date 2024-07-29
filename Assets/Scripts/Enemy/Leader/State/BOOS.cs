using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Skill[] phase1Skills; // 第一阶段的技能数组
    public Skill[] phase2Skills; // 第二
    private int currentPhase = 1; // 记录阶段
    public float skillInterval;//全局技能释放间隔，确保不会同时释放多个技能
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
        //让护盾罩在Boos上
        //postion.position = transform.position;

        currentHealth = GetComponent<Character>().currentHealth;
        if (currentHealth <= maxHealth / 2 && currentPhase == 1)
        {
            Phase2();
        }

        //自主进行技能释放的判断
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
         
        //提升属性,技能强化，技能间隔缩短
        //maxHealth *= 2;
        currentHealth = maxHealth;
        skillInterval -= 5f;
        Debug.Log("第二阶段");
    }

    //手动释放技能，可有可无
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

    //当技能冷却以及释放间隔结束，释放技能
    private void WaitSkill()
    {
        Skill[] currentSkills = currentPhase == 1 ? phase1Skills : phase2Skills;

        foreach (Skill skill in currentSkills)
        {
            if (skill.CanUseSkill() )
            {
                Debug.Log(skill.name);
                skill.TryUseSkill();
                break; // 确保技能不同时释放
            }
        }
    }
}

