using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainScene : MonoBehaviour
{
    //public GameObject _panel;
    public Image panel;
    public Image cut;

    public Sprite main;

     [SerializeField]
    public Sprite[] cuts = new Sprite[3];



    public GameObject buttons;

    public void Start() 
    {
        buttons.SetActive(false);
        panel.color = new Color(0,0,0,1);
        StartCoroutine( MainCutScene() );
    }

    IEnumerator MainCutScene()
    {        
        float fadeCount = 1;

        for(int i = 0 ; i < cuts.Length ; i++)
        {
            fadeCount = 1;
            panel.color = new Color(0,0,0,fadeCount);
            cut.sprite = cuts[i];
            
            while (fadeCount > 0.0f)
            {
                fadeCount -= 0.01f;
                yield return new WaitForSeconds(0.03f);
                panel.color = new Color(0,0,0,fadeCount);
            }

            yield return new WaitForSeconds(1.0f);
        }
        //_panel.SetActive(false);
        cut.sprite = main;
        buttons.SetActive(true);
    }
}
