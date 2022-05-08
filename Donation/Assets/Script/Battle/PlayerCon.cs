using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    GameObject playerManager;
    GameObject player;
    GameObject swordRad;
    GameObject attack;
    
    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager");
        player = GameObject.Find("Player");
        swordRad = GameObject.Find("SwordRotate");
    }

    void FixedUpdate()
    {
        PlayerMovement();
        Aim();
    }

    void PlayerMovement() //
    {
        if (Input.GetKey(KeyCode.UpArrow))
            player.transform.Translate(Vector2.up * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.LeftArrow))
            player.transform.Translate(Vector2.left * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.DownArrow))
            player.transform.Translate(Vector2.down * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.RightArrow))
            player.transform.Translate(Vector2.right * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);
    }

 
    public float rotateDegree;
    public void Aim()
    {
        if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            rotateDegree = 45;
        //N.E.
        else if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            rotateDegree = 135;
        //S.W.
        else if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            rotateDegree = 315;
        //N.W.
        else if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            rotateDegree = 225;
        //S.
        else if (Input.GetKey(KeyCode.DownArrow))
            rotateDegree = 0;
        //E.
        else if (Input.GetKey(KeyCode.RightArrow))
            rotateDegree = 90;
        //N.
        else if (Input.GetKey(KeyCode.UpArrow))
            rotateDegree = 180;
        //W.
        else if (Input.GetKey(KeyCode.LeftArrow))
            rotateDegree = 270;

        swordRad.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree - 90);
    }
}
