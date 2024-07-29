using UnityEngine;
using UnityEngine.EventSystems;


public class SkillButton : MonoBehaviour, IPointerClickHandler
{
    public SkillData skillData;//当前按钮的信息

    //点击后
    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.Instance.activeSkill = skillData;
        SkillManager.Instance.skillInform();
    }
}
