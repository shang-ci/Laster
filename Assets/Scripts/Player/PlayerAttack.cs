using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack: MonoBehaviour
{
    public float knockbackForc;
    public float attackRange;
    public int attackDamage;
    public LayerMask enemyLayer;
    private Attack attackComponent;


    private void Start()
    {
        attackComponent = GetComponent<Attack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            Ack();
        }
    }

    //¹¥»÷
    public void Ack()
    {
        //attackAudioSource.Play();
        attackComponent.Attacks(transform, attackRange, knockbackForc, attackDamage, enemyLayer);
    }

    //»­³ö¹¥»÷·¶Î§
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange );
    }
}
