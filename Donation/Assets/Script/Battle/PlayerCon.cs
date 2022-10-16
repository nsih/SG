using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    public GameObject playerManager;
    public GameObject swordRad;
    public GameObject attack;
    public bool check = true;
    public float cooltime;
    float curtime;
    public float invincibleTime = 1.5f; // 피격 시 무적시간
    bool attackedCheck = false;


    void Awake()
    {
        swordRad = gameObject.transform.GetChild(0).gameObject;
        attack = swordRad.gameObject.transform.GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        PlayerMovement();
        Aim();
        Attack();
    }

    void PlayerMovement() 
    {
        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
            transform.Translate(Vector2.up * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            transform.Translate(Vector2.left * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            transform.Translate(Vector2.down * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.Translate(Vector2.right * Time.deltaTime * playerManager.GetComponent<PlayerInfo>().moveSpeed);
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
                //attack 컴포넌트의 몹 공격 코루틴 실행
                attack.GetComponent<Attack>().StartCoroutine(attack.GetComponent<Attack>().SwingSword());
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

    IEnumerator attacked()
    {
        yield return new WaitForSeconds(invincibleTime);
        attackedCheck = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Enemy" && !attackedCheck)
        {
            playerManager.GetComponent<PlayerInfo>().curHP -= 1000;
            attackedCheck = true;
            StartCoroutine(attacked());
        }
    }

}
