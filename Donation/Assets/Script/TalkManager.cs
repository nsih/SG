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


//////////////////////////////////////////////////////////////////////////////////////////

    void GenerateData()
    {
        //  스트리머말 : 스트리머 이미지 : 슈퍼챗 활성여부 : 슈퍼챗 말
        talkData.Add(0, new string[] 
        {
            "안녕하세요 여러분,1,0,",
            "방송 시작하겠습니다,2,0,"});

        talkData.Add(1, new string[] 
        {
            "잠깐만요 이게뭐지?,0,1,.....",
            "........,0,1,hello"});

        talkData.Add(2, new string[] 
        {
            "......,0,1,im the god",
            "??....,3,1,im the god"});


        emoteData.Add(0,emoteArr[0]);
        emoteData.Add(1,emoteArr[1]);
        emoteData.Add(2,emoteArr[2]);
        emoteData.Add(3,emoteArr[3]);
        
        //emoteData.Add(0, });
    }

}
