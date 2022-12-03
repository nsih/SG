using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    public GameObject playerManager;
    public GameObject swordRad;
    public GameObject attack;
    SpriteRenderer attackSprite;
    SpriteRenderer playerSprite;
    public bool check;
    public float attackDamage;
    public float defense;
    public float cooltime;
    public float invincibleTime = 1.5f; // 피격 시 무적시간
    public float itemInvincibleTime = 2.5f;
    bool attackedCheck = false;
    public bool aimChoice = false;
    Vector2 _mousePos, _playerPos;


    void Awake()
    {
        swordRad = gameObject.transform.GetChild(0).gameObject;
        attack = swordRad.gameObject.transform.GetChild(0).gameObject;
        attackSprite = attack.GetComponent<SpriteRenderer>();
        playerSprite = this.gameObject.GetComponent<SpriteRenderer>();
        attackDamage = 1;
        check = true;
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
        if(!aimChoice)
        {
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
                rotateDegree = -45;
            //N.E.
            else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
                rotateDegree = 45;
            //S.W.
            else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
                rotateDegree = 225;
            //N.W.
            else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
                rotateDegree = 135;
            //S.
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                rotateDegree = -90;
            //E.
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                rotateDegree = 0;
            //N.
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                rotateDegree = 90;
            //W.
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                rotateDegree = 180;

        }
        else
        {
            _mousePos = Input.mousePosition;
            _playerPos = this.gameObject.transform.position;

            Vector3 target = Camera.main.ScreenToWorldPoint(_mousePos);

            float dy = target.y - _playerPos.y;
            float dx = target.x - _playerPos.x;

            rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        }
        
        swordRad.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
    }
        
   
    
    public void Attack()
    {
        if (check && Input.GetKey(KeyCode.E))
        {
            check = false;
            aimChoice = false;
            //attack 컴포넌트의 몹 공격 코루틴 실행
            attack.GetComponent<Attack>().StartCoroutine(attack.GetComponent<Attack>().SwingSword());
            StartCoroutine(attackCooldown());
        }
        else if(check && Input.GetMouseButton(0))
        {
            check = false;
            aimChoice = true;
            //attack 컴포넌트의 몹 공격 코루틴 실행
            attack.GetComponent<Attack>().StartCoroutine(attack.GetComponent<Attack>().SwingSword());
            StartCoroutine(attackCooldown());
        }
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(cooltime);
        check = true;
    }

    IEnumerator attacked()
    {
        StartCoroutine(Blinking());
        yield return new WaitForSeconds(invincibleTime);
        attackedCheck = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Enemy" && !attackedCheck)
        {
            playerManager.GetComponent<PlayerInfo>().curHP -= 1000-(1000*(defense/100)); //방어도에 따른 HP감소 계산 및 적용 
            attackedCheck = true;
            StartCoroutine(attacked());
        }
    }
    IEnumerator Blinking()
    {
        float countTime = 0;
        float blinkTic = 0.5f;
        if (invincibleTime < blinkTic) blinkTic = invincibleTime / 2.0f;

        while (countTime < itemInvincibleTime)
        {
            if ((countTime / blinkTic) % 2 == 0) 
                playerSprite.color = new Color(1, 1, 1, 0.4f);
            else
                playerSprite.color = new Color(1, 1, 1, 0.8f);

            yield return new WaitForSeconds(blinkTic);

            countTime += blinkTic;
            //Debug.Log(countTime);
        }

        playerSprite.color = new Color32(255, 255, 255, 255);

        yield return null;
    }

    //무적 아이템 사용
    public void InItemUsed()
    {
        attackedCheck = true;
        StartCoroutine(InvincivleUsed());
    }

    //무적 시간 2.5초 동안 스프라이트 알파값 0.4 유지
    protected IEnumerator InvincivleUsed()
    {
        float countTime = 0;
        float blinkTic = 0.5f;
        if (invincibleTime < blinkTic) blinkTic = invincibleTime / 2.0f;

        while (countTime < itemInvincibleTime)
        {
            if ((countTime / blinkTic) % 2 == 0)
                playerSprite.color = new Color(1, 1, 1, 0.4f);

            yield return new WaitForSeconds(blinkTic);

            countTime += blinkTic;
            //Debug.Log(countTime);
        }
        attackedCheck = false;
        playerSprite.color = new Color32(255, 255, 255, 255);

        yield return null;
    }

    //60초간 방어력 30% 증가
    public void IcreaseDefensive()
    {
        defense += 30;
        Invoke("DecreaseDefensive", 30);
    }

    //방어력 증가 해제
    public void DecreaseDefensive()
    {
        defense -= 30;
    }

    public void HealthUsed()
    {
        StartCoroutine(HealthRegen());
    }

    IEnumerator HealthRegen()
    {
       
        float countTime = 0;
        float regenTic = 1f;

        while (countTime < 12)
        {
            if ((countTime / regenTic) % 2 == 0)
            {
                if (playerManager.GetComponent<PlayerInfo>().curHP != playerManager.GetComponent<PlayerInfo>().maxHP)
                {
                    playerManager.GetComponent<PlayerInfo>().curHP += playerManager.GetComponent<PlayerInfo>().maxHP * 0.05f;
                }

                else if(playerManager.GetComponent<PlayerInfo>().curHP == playerManager.GetComponent<PlayerInfo>().maxHP)
                    yield return 0;
            }

            yield return new WaitForSeconds(regenTic);

            countTime += regenTic;
        }
    }

}
