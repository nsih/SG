using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float cooltime;
    float curtime;

    //공격 범위 내 몹 정보를 list로 저장 후 이벤트 처리
    private HashSet<Enemy> list = null;
    // Start is called before the first frame update
    void Awake()
    {
        cooltime = transform.GetComponentInParent<PlayerCon>().cooltime;
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


    public void OnTriggerStay2D(Collider2D collision)
    {
        /*
        수정 이전 코드(1타겟 공격만 가능)
        //for(int i = 0;i< collision.Length; i++)
        {
            if (collision.tag == "Enemy")
            {
                //Debug.Log(collision.Length);
                if (gameObject.GetComponentInParent<PlayerCon>().check == false && curtime <= 0)
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

    public void AttackEnemy(HashSet<Enemy> enemylist)
    {
        if (transform.GetComponentInParent<PlayerCon>().check == false && curtime <= 0)
        {
            curtime = cooltime;
            foreach (Enemy enemy in enemylist)
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

}
