using System.Collections.Generic;
using UnityEngine;

// 第一阶段技能一：发射飞弹并生成仆从
public class LeaderSkill1_1 : Skill
{
    public GameObject missilePrefab; 
    public GameObject minionPrefab; 
    public Transform target; 
    public Transform MinionPoint; // 仆从出生点
    public int maxMinions = 5; 
    public GameObject orbPrefab; 
    protected List<GameObject> minions = new List<GameObject>(); // 当前场上仆从列表
    protected List<GameObject> orbs = new List<GameObject>(); // 当前场上法球列表

    public override void UseSkill()
    {
        Debug.Log("skill1开始");

        // 检查所有必需的引用是否已分配
        if (missilePrefab == null)
        {
            Debug.Log("导弹预制体为空");
            return;
        }
        if (target == null)
        {
            Debug.Log("目标为空");
            return;
        }
        if (minionPrefab == null)
        {
            Debug.Log("仆从预制体为空");
            return;
        }
        if (MinionPoint == null)
        {
            Debug.Log("仆从出生点为空");
            return;
        }
        
        // 发射导弹
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("发射导弹");
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }

        // 生成仆从
        if (minions.Count < maxMinions)
        {
            Debug.Log("生成奴仆");
            GameObject minion = Instantiate(minionPrefab, MinionPoint.position, Quaternion.identity);
            minions.Add(minion);

            // 订阅仆从的死亡事件
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }
    //仆从死亡会调用
    protected void HandleMinionKilled(GameObject minion)
    {
        // 从仆从列表中移除已死亡的仆从
        minions.Remove(minion);

        // 死亡的仆从化成生成法球
        if (orbs.Count < 8)
        {
            if (orbPrefab == null)
            {
                Debug.Log("法球预制体为空");
                return;
            }
            Debug.Log("仆从死了，生曾护盾");
            // 实例化法球并添加到法球列表中
            GameObject orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
            orb.GetComponent<Orb>().Initialize(transform);
            orbs.Add(orb);
        }
    }
}