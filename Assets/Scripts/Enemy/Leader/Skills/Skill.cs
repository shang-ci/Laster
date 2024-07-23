using UnityEngine;

// 基础技能类
public abstract class Skill : MonoBehaviour
{
    public float cooldown;
    private float lastuseTime = 0f;

    public abstract void UseSkill();

    //判断技能是否可以释放
    public bool CanUseSkill()
    {
        return Time.time > lastuseTime + cooldown ;
    }

    //计算技能冷却
    protected void CoolDown()
    {
        lastuseTime = Time.time;
    }

    //释放技能
    public void TryUseSkill()
    {
        Debug.Log("Using skill: " + this.GetType().Name);
        UseSkill();
        CoolDown();
    }
}



