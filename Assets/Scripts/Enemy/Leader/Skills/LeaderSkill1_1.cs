using System.Collections.Generic;
using UnityEngine;

// ��һ�׶μ���һ������ɵ��������ʹ�
public class LeaderSkill1_1 : Skill
{
    public GameObject missilePrefab; 
    public GameObject minionPrefab; 
    public Transform target; 
    public Transform MinionPoint; // �ʹӳ�����
    public int maxMinions = 5; 
    public GameObject orbPrefab; 
    protected List<GameObject> minions = new List<GameObject>(); // ��ǰ�����ʹ��б�
    protected List<GameObject> orbs = new List<GameObject>(); // ��ǰ���Ϸ����б�

    public override void UseSkill()
    {
        Debug.Log("skill1��ʼ");

        // ������б���������Ƿ��ѷ���
        if (missilePrefab == null)
        {
            Debug.Log("����Ԥ����Ϊ��");
            return;
        }
        if (target == null)
        {
            Debug.Log("Ŀ��Ϊ��");
            return;
        }
        if (minionPrefab == null)
        {
            Debug.Log("�ʹ�Ԥ����Ϊ��");
            return;
        }
        if (MinionPoint == null)
        {
            Debug.Log("�ʹӳ�����Ϊ��");
            return;
        }
        
        // ���䵼��
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("���䵼��");
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().SetTarget(target);
        }

        // �����ʹ�
        if (minions.Count < maxMinions)
        {
            Debug.Log("����ū��");
            GameObject minion = Instantiate(minionPrefab, MinionPoint.position, Quaternion.identity);
            minions.Add(minion);

            // �����ʹӵ������¼�
            minion.GetComponent<Minion>().OnMinionKilled += HandleMinionKilled;
        }
    }
    //�ʹ����������
    protected void HandleMinionKilled(GameObject minion)
    {
        // ���ʹ��б����Ƴ����������ʹ�
        minions.Remove(minion);

        // �������ʹӻ������ɷ���
        if (orbs.Count < 8)
        {
            if (orbPrefab == null)
            {
                Debug.Log("����Ԥ����Ϊ��");
                return;
            }
            Debug.Log("�ʹ����ˣ���������");
            // ʵ����������ӵ������б���
            GameObject orb = Instantiate(orbPrefab, transform.position, Quaternion.identity);
            orb.GetComponent<Orb>().Initialize(transform);
            orbs.Add(orb);
        }
    }
}