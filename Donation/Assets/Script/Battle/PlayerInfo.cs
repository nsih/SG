using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerInfo = null;

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

        moveSpeed = 5;
    }

    public int color;    
    public int atk;
    public int moveSpeed;

    public float Gauge;
}
