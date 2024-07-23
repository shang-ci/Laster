using UnityEngine;

// 第二阶段技能一：强化仆从生成技能
public class LeaderSkill2_1 : LeaderSkill1_1
{
    public override void UseSkill()
    {
        Debug.Log("skill3");
        // 发射更多的飞弹
        for (int i = 0; i < 10; i++)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }
        //仆从增强
        if (minions.Count < maxMinions)
        {
            GameObject minion = Instantiate(minionPrefab, MinionPoint.position, Quaternion.identity);

            // 增强仆从属性
            minion.GetComponent<Minion>().Enhance();
            minions.Add(minion);

            // 订阅仆从死亡事件->生成法球
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }
}