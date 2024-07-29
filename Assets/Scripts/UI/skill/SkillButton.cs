using UnityEngine;
using UnityEngine.EventSystems;


public class SkillButton : MonoBehaviour, IPointerClickHandler
{
    public SkillData skillData;//��ǰ��ť����Ϣ

    //�����
    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.Instance.activeSkill = skillData;
        SkillManager.Instance.skillInform();
    }
}
