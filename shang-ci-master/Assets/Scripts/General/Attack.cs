using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    //������Χ������Ƶ��
    public float attackRange;
    public float attackRate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //ȷ�������Ķ���������������﷨��
        collision.GetComponent<Character>()?.TakeDamage(this);
    }

}
