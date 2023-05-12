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
    public int eventEndNum = 0;


    public bool isTalkBar;
    public bool isSuperChat;
    public int talkIndex;


    public void Start()
    {
        SetEventNum(0,3);   //0~3번 이벤트 출력.
    }

    public void Update() 
    {
        if(Input.GetKeyDown( KeyCode.E))
        {
            if(isTyping == false)
            {
                Talk(3);
                talkBar.SetActive(isTalkBar);
                superChatBar.SetActive(isSuperChat);
            }
        }
    }


    public void Talk(int endNum)  //start ~ end 넘버 이벤트까지 출력
    {
        string talkData;

        string streamerDialogText;

        if(eventNum < endNum)
        {
            isTalkBar = true;

            talkData = talkManager.GetTalk(eventNum, talkIndex);

            if(talkData == null)    //이벤트 끝 체크
            {   
                isTalkBar = false;
                isSuperChat = false;
                talkText.text = null;
                SuperChatText.text = null;
                streamerImg.sprite = talkManager.GetEmote(0);

                eventNum ++;        //다음 이벤트++
                talkIndex = 0;      //인덱스 초기화
                return;
            }
            else
            {
                streamerDialogText = talkData.Split(',')[0];
                StartCoroutine( typing(streamerDialogText));

                streamerImg.sprite = talkManager.GetEmote( int.Parse(talkData.Split(',')[1]));
                Donation( int.Parse (talkData.Split(',')[2]) );
                SuperChatText.text = talkData.Split(',')[3];

                talkIndex ++;   //다음인덱스
                return;
            }
        }

        else if(eventNum == endNum)
        {
            if(endNum == 3) //일단 초기화
            {
                eventNum = 0;
            }
            return;
        }

    }


    public bool isTyping = false;
    IEnumerator typing(string dialogText)
    {
        for(int i = 0 ; i <=  dialogText.Length ; i++)
        {
            isTyping = true;
            talkText.text = dialogText.Substring(0,i);
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    void Donation( int superChat )
    {
        if ( superChat == 0)
            isSuperChat = false;
        
        else if (superChat == 1)
            isSuperChat = true;
    }

    public void SetEventNum(int start, int end)
    {
        eventNum = start;
        eventEndNum = end;
    }
}
