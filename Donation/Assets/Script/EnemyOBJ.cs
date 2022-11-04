using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOBJ : MonoBehaviour
{
    GameObject stageManager;

    float speed;

    void OnEnable()
    {
        stageManager = GameObject.Find("StageManager");

    }


    public void Dead() //적죽음
    {
        this.gameObject.SetActive(false);

        stageManager.GetComponent<StageManager>().slainCount++;
    }
}
