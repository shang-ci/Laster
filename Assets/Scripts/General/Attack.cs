using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attack : MonoBehaviour
{
    public static Attack instance;



    public void Attacks(Transform attack, float attackRange, float knockForc, int damage,LayerMask targetLayer)
    {
        //检测敌人
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, targetLayer);

        foreach(Collider2D enemy in enemies)
        {
            //判断是否空
            if(enemy ==  null) continue;

            //获得碰撞物体
            Character character = enemy.GetComponent<Character>();
            if(character != null)
            {
                character.TakeDamage(damage);
                //碰撞的反弹方向
                //Vector2 dir = new((enemy.transform.position.x - transform.position.x),0);
                //character.Knockback(dir,knockForc);
            }
        }
    }
}
