using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOBJ : MonoBehaviour
{
    GameObject player;
    GameObject stageManager;

    float speed;
    float time;

    void OnEnable()
    {
        player = GameObject.Find("Player");
        stageManager = GameObject.Find("StageManager");


        speed = 5;
        time = 0;
    }

    void Update()
    {
        OffTimer();
    }
    /*
    void FixedUpdate() 
    {
        Movement();
    }
    */

    void OffTimer() 
    {
        time += Time.deltaTime;

        if(time > 5.0f)
            Dead();
    }

    void Movement()
    {
        Vector2 player_Pos = player.transform.position;
        Vector2 enemy_Pos = this.transform.position;

        this.gameObject.transform.Translate((player_Pos - enemy_Pos).normalized * speed * Time.deltaTime);
    }

    private void Dead() //적죽음
    {
        this.gameObject.SetActive(false);

        stageManager.GetComponent<StageManager>().slainCount++;
    }
}
