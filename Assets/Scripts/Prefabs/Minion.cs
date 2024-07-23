using UnityEngine;

public class Minion : EnemyAI
{
    // 定义仆从被击杀的事件
    public delegate void MinionKilledHandler(GameObject minion);
    public event MinionKilledHandler OnMinionKilled;


    //在character的死亡事件里触发Die
    public void Die()
    {
            // 触发仆从死亡事件
            if (OnMinionKilled != null)
            {
                OnMinionKilled(gameObject);
            }
            Debug.Log("仆从死了");
            Destroy(gameObject);      
    }

    //调用charater的属性
    public void Enhance()
    {
        // 增强仆从的属性
        GetComponent<Character>().currentHealth *= 3;
        Debug.Log("Minion enhanced");
    }
}

