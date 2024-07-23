using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange;
    public float knockForc;
    public int attackDamage;
    public LayerMask playerLayer;
    public float attackCooldown;//��ȴ

    private float nextAttackTime = 0f;//��һ�����㣨�����

    private Attack attackComponent;

    private void Start()
    {
        attackComponent = GetComponent<Attack>();
    }

    private void Update()
    {
        //
        if(Time.time > nextAttackTime)
        {
            Ack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void Ack()
    {
        attackComponent.Attacks(transform, attackRange, knockForc, attackDamage, playerLayer);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
