using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreamingManager : MonoBehaviour
{
    public Image streamerImg;

    public GameObject talkBar;
    public Text talkText;


    public GameObject superChatBar;
    public Text SuperChatText;


    public TalkManager talkManager;
    public int eventNum = 0;


    public bool isSuperChat;
    public int talkIndex;



    public void Update() 
    {
        if(Input.GetKeyDown( KeyCode.E ))
        {
            Talk(eventNum);
            talkBar.SetActive(true);
            superChatBar.SetActive(isSuperChat);
        }
    }


    void Talk(int en)
    {
        string talkData = talkManager.GetTalk(eventNum, talkIndex);

        if(talkData == null)    //이벤트 끝 체크
        {
            talkText.text = null;
            SuperChatText.text = null;

            isSuperChat = false;

            eventNum ++;        //다음 이벤트++
            talkIndex = 0;      //인덱스 초기화

            return;
        }

        Donation( int.Parse(talkData.Split(',')[2] ) ) ;             //텍스트가 스트리머인지 슈퍼챗인지 구분


        talkText.text = talkData.Split(',')[0];
        SuperChatText.text = talkData.Split(',')[3];
        streamerImg.sprite = talkManager.GetEmote( int.Parse(talkData.Split(',')[1]));

        talkIndex ++;   //다음인덱스
    }


    void Donation( int superChat )
    {
        if ( superChat == 0)
            isSuperChat = false;
        
        else if (superChat == 1)
            isSuperChat = true;
            
    }


    void Chat()
    {

    }
}
