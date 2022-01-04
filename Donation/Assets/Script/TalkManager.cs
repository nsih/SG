using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TalkManager : MonoBehaviour
{
    Dictionary<int,string[]> talkData;

    Dictionary<int,Sprite> emoteData;

    public Sprite[] emoteArr;

    void Awake() 
    {
        talkData = new Dictionary<int, string[]>();
        emoteData = new Dictionary<int, Sprite>();


        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(0, new string[] 
        {
            "0번 이벤트의 1번 - 보통:0",
            "0번 이벤트의 2번 - 기쁨:1"});

        talkData.Add(1, new string[] 
        {
            "1번 이벤트의 1번 - 슬픔:2",
            "1번 이벤트의 2번 - 화남:3"});

        talkData.Add(2, new string[] 
        {
            "2번 이벤트의 1번 - 보통:0",
            "2번 이벤트의 2번 - 보통:0"});


        emoteData.Add(0,emoteArr[0]);
        emoteData.Add(1,emoteArr[1]);
        emoteData.Add(2,emoteArr[2]);
        emoteData.Add(3,emoteArr[3]);
        
        //emoteData.Add(0, });
    }


    public string GetTalk(int eventNum, int talkIndex)
    {
        if(talkIndex == talkData[eventNum].Length)
        {
            return null;
        }

        else
        {
            return talkData[eventNum][talkIndex];
        }
    }

    public Sprite GetEmote(int id)
    {
        return emoteData[id];
    }

}
