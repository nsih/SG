using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    //공격 범위 내 몹 정보를 list로 저장 후 이벤트 처리
    List<GameObject> list = new List<GameObject>();
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0.3f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy != null && enemy.tag == "Enemy")
        {
            Debug.Log("AddList name" + enemy);
            this.list.Add(enemy);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy != null && this.list.Contains(enemy) == true)
        {
            Debug.Log("RemoveList name : " + enemy.name);
            this.list.Remove(enemy);
        }
    }

    void AttackEnemy(List<GameObject> enemyList)
    {
        if (transform.GetComponentInParent<PlayerCon>().check == false )
        {
            foreach (GameObject enemy in enemyList)
            {
                Enemy temp = enemy.GetComponent<Enemy>();
                if (!temp.attackCheck)
                {
                    Debug.Log("Attacked name : " + enemy.name);
                    temp.Attack();
                    
                }
            }
            list.Clear();
        }
    }

    public IEnumerator SwingSword()
    {
        if (list != null)
        {
            AttackEnemy(list);
            sprite.color = new Color(1, 1, 1, 1);
        }
        yield return new WaitForSeconds(0.3f);
        sprite.color = new Color(1, 1, 1, 0.3f);
        //오브젝트 새로고침
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);

    }
}
