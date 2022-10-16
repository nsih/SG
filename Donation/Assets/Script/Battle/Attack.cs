using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Enemy>().hp--;
        }
    }
}