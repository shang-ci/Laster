using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private int cherrys = 0;
    [SerializeField] private Text cherryText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherrys++;
            cherryText.text = "cherries:" + cherrys;
        }
    }
 
}
