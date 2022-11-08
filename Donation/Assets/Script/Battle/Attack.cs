using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float cooltime;
    float curtime;
    public GameObject PlayerManager;
    private HashSet<Enemy> list = null;
    // Start is called before the first frame update
    void Awake()
    {
        cooltime = PlayerManager.GetComponent<PlayerCon>().cooltime;
        this.list = new HashSet<Enemy>();
    }
    
    void FixedUpdate()
    {
        curtime -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        if (enemy != null && enemy.tag == "Enemy")
        {
            //Debug.Log("AddList name" + enemy);
            this.list.Add(enemy);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        if(enemy != null && this.list.Contains(enemy) == true)
        {
            //Debug.Log("RemoveList name : " + enemy.name);
            this.list.Remove(enemy);
        }
    }

    public void AttackEnemy(HashSet<Enemy> enemylist)
    {
        if (PlayerManager.GetComponent<PlayerCon>().check == false && curtime <= 0)
        {
            curtime = cooltime;
            foreach(Enemy enemy in enemylist)
            {
                if (!enemy.attackCheck)
                {
                    //Debug.Log("Attacked name : " + enemy.name);
                    enemy.attackCheck = true;
                    enemy.Attack();
                    enemy.hp--;
                }
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        /*
        //Collider2D[] mobs = Physics2D.OverlapCollider(Collider2D collision, ContactFilter2D cF, Collider2D[] result)
        //for(int i = 0;i< collision.Length; i++)
        {
            if (collision.tag == "Enemy")
            {
                //Debug.Log(collision.Length);
                if (PlayerManager.GetComponent<PlayerCon>().check == false && curtime <= 0)
                {
                    curtime = cooltime;
                    if (!collision.gameObject.GetComponent<Enemy>().attackCheck)
                    {
                        collision.gameObject.GetComponent<Enemy>().attackCheck = true;
                        collision.gameObject.GetComponent<Enemy>().Attack();
                        collision.gameObject.GetComponent<Enemy>().hp--;
                    }
                }
            }
        }
        */
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        if (list != null)
        {
            AttackEnemy(list);
        }
    }

   
}
