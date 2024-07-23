using UnityEngine;

// �ڶ��׶μ���һ��ǿ���ʹ����ɼ���
public class LeaderSkill2_1 : LeaderSkill1_1
{
    public override void UseSkill()
    {
        Debug.Log("skill3");
        // �������ķɵ�
        for (int i = 0; i < 10; i++)
        {
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }
        //�ʹ���ǿ
        if (minions.Count < maxMinions)
        {
            GameObject minion = Instantiate(minionPrefab, MinionPoint.position, Quaternion.identity);

            // ��ǿ�ʹ�����
            minion.GetComponent<Minion>().Enhance();
            minions.Add(minion);

            // �����ʹ������¼�->���ɷ���
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }
}