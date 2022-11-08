using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    GameObject playerManager;
    GameObject player;
    GameObject swordRad;
    GameObject sword;
    GameObject attack;
    public bool check=true;
    public float cooltime;
    float curtime;
    
    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager");
        sword = GameObject.Find("attackE");
        player = GameObject.Find("Player");
        swordRad = GameObject.Find("SwordRotate");
        attack = swordRad.transform.GetChild(0).gameObject;

    }

    void FixedUpdate()
    {
        PlayerMovement();
        Aim();
        Attack();
    }

    void PlayerMovement() //
    {
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
            player.transform.Translate(Vector2.up * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            player.transform.Translate(Vector2.left * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            player.transform.Translate(Vector2.down * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            player.transform.Translate(Vector2.right * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);
    }

 
    public float rotateDegree;
    public void Aim()
    {
        if(check == true)
        {
            attack.GetComponent<SpriteRenderer>().color = new Color(36f / 255f, 36f / 255f, 36f / 255f);
        }
        else
        {
            attack.GetComponent<SpriteRenderer>().color = new Color(219f / 255f, 219f / 255f, 219f / 255f);
        }
        if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
            rotateDegree = 45;
        //N.E.
        else if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
            rotateDegree = 135;
        //S.W.
        else if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
            rotateDegree = 315;
        //N.W.
        else if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
            rotateDegree = 225;
        //S.
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            rotateDegree = 0;
        //E.
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rotateDegree = 90;
        //N.
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            rotateDegree = 180;
        //W.
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rotateDegree = 270;

        swordRad.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree - 90);
    }
    
    public void Attack()
    {
        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.E) && check)
            {
                check = false;
                StartCoroutine(aimColor());
                curtime = cooltime;
            }
        }
        curtime -= Time.deltaTime;
    }

    IEnumerator aimColor()
    {
        yield return new WaitForSeconds(0.3f);
        check = true;
    }
}
