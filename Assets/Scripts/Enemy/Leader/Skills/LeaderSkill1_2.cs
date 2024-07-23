using System.Collections.Generic;
using UnityEngine;

// 第一阶段技能二：献祭仆从生成护盾
public class LeaderSkill1_2 : Skill
{
    public GameObject shieldPrefab; 
    public Transform leaderTransform; // 领袖的位置
    private List<GameObject> minions = new List<GameObject>(); // 当前场上的仆从列表

    public override void UseSkill()
    {
        Debug.Log("skill2 minion标记");
        minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));

        //消耗仆从补充血量和护盾
        float shieldValue = minions.Count * 200f;

        Debug.Log("献祭仆从");
        foreach (GameObject minion in minions)
        {
            Destroy(minion);
        }

        // 清空仆从列表
        minions.Clear();

        GameObject shield = Instantiate(shieldPrefab, leaderTransform.position, Quaternion.identity);
        shield.GetComponent<Shield>().SetShieldValue(shieldValue);

        shield.transform.SetParent(leaderTransform);
    }
}