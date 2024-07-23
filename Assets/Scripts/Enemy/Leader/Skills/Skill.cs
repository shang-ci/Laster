using UnityEngine;

// ����������
public abstract class Skill : MonoBehaviour
{
    public float cooldown;
    private float lastuseTime = 0f;

    public abstract void UseSkill();

    //�жϼ����Ƿ�����ͷ�
    public bool CanUseSkill()
    {
        return Time.time > lastuseTime + cooldown ;
    }

    //���㼼����ȴ
    protected void CoolDown()
    {
        lastuseTime = Time.time;
    }

    //�ͷż���
    public void TryUseSkill()
    {
        Debug.Log("Using skill: " + this.GetType().Name);
        UseSkill();
        CoolDown();
    }
}



