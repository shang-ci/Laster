using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    public SkillData activeSkill;//��ǰ����ļ���

    [Header("UI��Ϣ")]
    public Text skillNameText, skillLevText, skillDesText;

    [Header("skillPoint")]
    [SerializeField] private int skillPoint;
    public Text pointText;

    public SkillButton[] skillButtons;

    //����ģʽ
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdatePoint();
    }

    //������ť
    public void UpgradeButton()
    {
        if(activeSkill == null)
            return;
        //skill1&&skill2
        if(skillPoint >= 0 && activeSkill.perSkills.Length == 0)
        {
            skillUpdate();
        }
        //skill3--skill6
        if(skillPoint > 0)
        {
            for(int i = 0; i<activeSkill.perSkills.Length; i++)
            {
                if (activeSkill.perSkills[i].isUnlocked == true)
                {
                    skillUpdate();
                    break;
                }
            }
        }
    }

    //���¼���
    public void skillUpdate()
    {
        skillButtons[activeSkill.skillID].GetComponent<Image>().color = Color.red;
        skillButtons[activeSkill.skillID].transform.GetChild(1).gameObject.SetActive(true);
        activeSkill.skillLevel++;
        skillButtons[activeSkill.skillID].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = activeSkill.skillLevel.ToString();

        skillInform();

        skillPoint--;
        UpdatePoint();

        activeSkill.isUnlocked = true;

    }

    //������Ϣ������
    public void skillInform()
    {
        skillLevText.text =  "SKILL LEVEL:" + activeSkill.skillLevel;
        skillNameText.text = activeSkill.skillName;
        skillDesText.text = "DESCRIPTION: \n" + activeSkill.skillDescription;
    }

    public void UpdatePoint()
    {
        pointText.text = "POINTS: " +  skillPoint + "/20" ;
    }
}
