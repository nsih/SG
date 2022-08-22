using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    //스테이지에 따라 바뀌는 스크립트 or UI들을 컨트롤.
    GameObject enemyManager;

    public int stage = 1;
    public int slainCount = 0;

    public Text stageUI;
    public Text slainCountUI;


    public 

    // Start is called before the first frame update
    void Awake()
    {
        enemyManager = GameObject.Find("BattleManager").transform.Find("EnemyManager").gameObject;

        enemyManager.SetActive(true);
        enemyManager.GetComponent<EnemyPollManager>().delayTime = 3.0f;
    }

    void Update() 
    {
        StageCon();
        UICon();
    }

    void StageCon()
    {
        if(slainCount >= 5)
        {
            enemyManager.SetActive(false);
            slainCount = 0;

            stage++;
            EnemyTypeCon();
            EnemyGenCon();

            enemyManager.SetActive(true);
        }
    }

    void MapCon()   {} //배경

    void EnemyTypeCon()
    {
        enemyManager.GetComponent<EnemyPollManager>().enemyType = 0;
    }

    void EnemyGenCon()
    {
        enemyManager.GetComponent<EnemyPollManager>().delayTime = enemyManager.GetComponent<EnemyPollManager>().delayTime - 0.5f;
    }


    void UICon()
    {
        stageUI.text = stage.ToString();
        slainCountUI.text = slainCount.ToString();
    }


/*
어떤 땅인가
어떤 몹이 나오는가?
몹이 어느정도 빈전하게 나오는가?
스테이지 넘어가는 조건은 무엇인가? (n마리 처치 or n초 버티기)
*/


}
