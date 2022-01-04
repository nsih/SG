using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreamingManager : MonoBehaviour
{
    public GameObject talkBar;
    public Text talkText;
    public Image streamerImg;

    public TalkManager talkManager;
    public int eventNum = 0;


    public bool isAction;
    public int talkIndex;

    public bool isStreamer;


    public void Awake()
    {
        isAction = false;
    }

    public void Update() 
    {
        if(Input.GetKeyDown( KeyCode.E ))
        {
            Action();
        }
    }

    public void Action()
    {
        Talk(eventNum);
        talkBar.SetActive(isAction);
    }

    void Talk(int en)
    {
        string talkData = talkManager.GetTalk(eventNum, talkIndex);

        if(talkData == null)
        {
            eventNum ++;        //인덱스 끝나면 다음이벤트
            talkIndex = 0;      //인덱스 초기화
            isAction = false;   //바 관련 불

            return;
        }

        if(isStreamer)  //superchat과 구분
        {
            talkText.text = talkData.Split(':')[0];
            streamerImg.sprite = talkManager.GetEmote( int.Parse(talkData.Split(':')[1]));
        }
        else
        {
            talkText.text = talkData.Split(':')[0];
            streamerImg.sprite = talkManager.GetEmote( int.Parse(talkData.Split(':')[1]));
        }

        talkIndex ++;   //다음인덱스
        isAction = true;
    }
}
