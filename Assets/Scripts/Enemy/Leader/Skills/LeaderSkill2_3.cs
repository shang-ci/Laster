using UnityEngine;

// 第二阶段技能三：近战爆炸技能
public class LeaderSkill2_3 : Skill
{
    public float explosionRange = 10f; 
    public int explosionDamage = 200; 

    public override void UseSkill()
    {
        Debug.Log("爆破");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<Character>().TakeDamage(explosionDamage);
            }
        }
    }
}