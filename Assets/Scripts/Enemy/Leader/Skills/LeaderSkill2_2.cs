using UnityEngine;

// �ڶ��׶μ��ܶ������ܲ���
public class LeaderSkill2_2 : Skill
{
    public int damage = 100; 
    //public float drain = 1f; // ����ֵ(����ʹ��)
    public float range = 20f; // ����Χ

    public override void UseSkill()
    {
        Debug.Log("skill4");
        // �ͷų����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<Character>().TakeDamage(damage);

                // ��ȡ����
                //enemy.GetComponent<Character>().UseMana(drain);
            }
        }
    }
}