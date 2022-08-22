using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float cooltime;
    float curtime;
    GameObject enemy;
    public GameObject PlayerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        curtime -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = collision.gameObject;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (PlayerManager.GetComponent<PlayerCon>().check == false && curtime <= 0)
            {
                enemy.GetComponent<Enemy>().hp--;
                curtime = cooltime;
                if (!enemy.GetComponent<Enemy>().attackCheck)
                {
                    enemy.GetComponent<Enemy>().attackCheck = true;
                    enemy.GetComponent<Enemy>().Attack();
                }
            }
        }
    }
   
}
