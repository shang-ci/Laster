using UnityEngine;

[CreateAssetMenu(menuName ="New Skill",fileName ="skill")]
public class SkillData : ScriptableObject
{
    public int skillID;
    public Sprite skillSprite;

    public string skillName;
    [TextArea(1,8)]
    public string skillDescription;
    public int skillLevel;
    public bool isUnlocked;
    public SkillData[] perSkills;
}
