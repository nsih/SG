using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerInfo = null;

    public int color;    
    public int atk;         //공격력
    public float defense;   //방어력
    public int moveSpeed;   //이동속도
    public float attackSpeed;   //공격속도

    public float Gauge;

    public Image HPBar;
    public float curHP;
    public float maxHP;
    TextMeshProUGUI curHPText;
    TextMeshProUGUI maxHPText;
    GameObject player;

    void Awake() 
    {
        if(playerInfo == null) //인스턴스가 씬에 없으면
        {
            playerInfo = this;  //자신을 instance로 넣는다.
            DontDestroyOnLoad(gameObject);  //onload에서 삭제하지 않는다.
        }
        
        else
        {
            if(playerInfo != this)  //이미 하나 존재하면
                Destroy(this.gameObject); //awake된 자신을 삭제
        }

        player = GameObject.FindGameObjectWithTag("Player");
        atk = 1;
        defense = 300;
        moveSpeed = 5;
        attackSpeed = 1;
        player.GetComponent<PlayerCon>().cooltime = 3.0f / attackSpeed;

        curHP = maxHP;
        curHPText = HPBar.GetComponentsInChildren<TextMeshProUGUI>()[0];
        maxHPText = HPBar.GetComponentsInChildren<TextMeshProUGUI>()[1];
    }

    void Update()
    {
        fillHPBar();
    }

    void fillHPBar()
    {
        HPBar.fillAmount = curHP / maxHP;
        //HPtext 소수점 반올림하여 표기
        curHPText.text = Mathf.Round(curHP).ToString();
        maxHPText.text = "/ " + Mathf.Round(maxHP).ToString();
    }

    void attacked()
    {

    }
}
