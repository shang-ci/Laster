using System.Collections.Generic;
using UnityEngine;

// ��һ�׶μ��ܶ����׼��ʹ����ɻ���
public class LeaderSkill1_2 : Skill
{
    public GameObject shieldPrefab; 
    public Transform leaderTransform; // �����λ��
    private List<GameObject> minions = new List<GameObject>(); // ��ǰ���ϵ��ʹ��б�

    public override void UseSkill()
    {
        Debug.Log("skill2 minion���");
        minions = new List<GameObject>(GameObject.FindGameObjectsWithTag("Minion"));

        //�����ʹӲ���Ѫ���ͻ���
        float shieldValue = minions.Count * 200f;

        Debug.Log("�׼��ʹ�");
        foreach (GameObject minion in minions)
        {
            Destroy(minion);
        }

        // ����ʹ��б�
        minions.Clear();

        GameObject shield = Instantiate(shieldPrefab, leaderTransform.position, Quaternion.identity);
        shield.GetComponent<Shield>().SetShieldValue(shieldValue);

        shield.transform.SetParent(leaderTransform);
    }
}