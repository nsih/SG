using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void OnclickButton_Start()
    {
        SceneManager.LoadScene("StreamingScene");

        Debug.Log("asdf");
    }

    public void OnclickButton_End()
    {

    }
}
