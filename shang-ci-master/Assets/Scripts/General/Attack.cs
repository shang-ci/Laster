using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    //攻击范围，攻击频率
    public float attackRange;
    public float attackRate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //确保触碰的对象有这个函数，语法糖
        collision.GetComponent<Character>()?.TakeDamage(this);
    }

}
