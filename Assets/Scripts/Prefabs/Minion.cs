using UnityEngine;

public class Minion : EnemyAI
{
    // �����ʹӱ���ɱ���¼�
    public delegate void MinionKilledHandler(GameObject minion);
    public event MinionKilledHandler OnMinionKilled;


    //��character�������¼��ﴥ��Die
    public void Die()
    {
            // �����ʹ������¼�
            if (OnMinionKilled != null)
            {
                OnMinionKilled(gameObject);
            }
            Debug.Log("�ʹ�����");
            Destroy(gameObject);      
    }

    //����charater������
    public void Enhance()
    {
        // ��ǿ�ʹӵ�����
        GetComponent<Character>().currentHealth *= 3;
        Debug.Log("Minion enhanced");
    }
}

