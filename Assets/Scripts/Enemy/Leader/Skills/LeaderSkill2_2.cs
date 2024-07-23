using UnityEngine;

// 第二阶段技能二：动能波动
public class LeaderSkill2_2 : Skill
{
    public int damage = 100; 
    //public float drain = 1f; // 技力值(放弃使用)
    public float range = 20f; // 波范围

    public override void UseSkill()
    {
        Debug.Log("skill4");
        // 释放冲击波
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<Character>().TakeDamage(damage);

                // 吸取技力
                //enemy.GetComponent<Character>().UseMana(drain);
            }
        }
    }
}