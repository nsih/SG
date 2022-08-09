using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPollManager : MonoBehaviour
{
    List<GameObject> items = new List<GameObject>();


    GameObject player;
    GameObject enemyManager;


    public GameObject enemy_000;

    public float timerTime;
    public float delayTime; //적스폰 시간

    bool isActivatable;


    void Awake()
    {
        player = GameObject.Find("Player");
        enemyManager = GameObject.Find("EnemyManager");
        
        delayTime = 1.0f;
        InitPool();
        isActivatable = false;
    }

    void Update()
    {
        ActivateTimer();

        if(isActivatable == true)
        {
            ActivateEnemy();
        }
    }


    public void InitPool()  //
    {
        for(int i = 0; i < 50 ; i++)
        {
            GameObject temp = Instantiate(enemy_000 as GameObject);
            temp.gameObject.SetActive(false);

            items.Add(temp);
            items[i].transform.SetParent(enemyManager.transform);
        }
    }

    public void ActivateEnemy()
    {
        for(int i = 0 ; i <= items.Count ; i++)
        {
            int act = 0;

            foreach (var item in items)
            {
                if(item.activeSelf == true)
                    act ++;
            }

            if(act == items.Count)  //풀 부족
            {
                Debug.Log("Pool error");
                break;
            }

            
            else if(items[i].activeSelf == false)   //비활성화 풀 탐색후 활성화
            {
                float rx = Random.Range(-15.0f,15.0f);
                float ry = Random.Range(-15.0f,15.0f);


                Vector3 pos = new Vector3(player.transform.position.x + rx,player.transform.position.y + ry,0f);

                Debug.Log(pos);

                items[i].transform.position = pos;
                items[i].SetActive(true);                        
                isActivatable = false;
                break;
            }
        }
    }

    public void DisposePool()
    {
        foreach(var item in items)
        {
            GameObject.Destroy(item);
        }

        items.Clear();
    }



     void ActivateTimer()
    {
        if(timerTime < delayTime)
            timerTime += Time.deltaTime;

        else
        {
            isActivatable = true;
            timerTime = 0;            
        }
    }
}
