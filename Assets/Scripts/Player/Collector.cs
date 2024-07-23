using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public int cherrys = 0;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            GetComponent<Character>().currentHealth += 400;
            GetComponent<PlayerAttack>().attackDamage += 10;
            Destroy(collision.gameObject);
            cherrys++;
        }
    }
 
}
