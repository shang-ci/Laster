using UnityEngine;

// �ڶ��׶μ���������ս��ը����
public class LeaderSkill2_3 : Skill
{
    public float explosionRange = 10f; 
    public int explosionDamage = 200; 

    public override void UseSkill()
    {
        Debug.Log("����");
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