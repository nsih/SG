using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float animSpeed = 0.5f;
    public Animator animator;
    public bool isTracing = false;
    public float movementFlag = 0;
    public float moveSpeed = 2.0f;
    public float damaged;
    public bool attackCheck = true; //몹이 플레이어에게 공격받았는지 확인하는 변수
    public float hp = 3;
    public float invincibleTime;
    public bool isInvincible = false;
    public float tracingDistance = 6;
    public GameObject player;
    public float distance;
    SpriteRenderer spriteRenderer;

    void Start()
    {
       damaged = GameObject.Find("Player").GetComponent<PlayerCon>().attackDamage;
    }

    protected virtual void OnEnable()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = 3;
        //invincibleTime = GameObject.Find("attackE").GetComponent<Attack>().cooltime;
        StartCoroutine("ChangeMovement");
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        animator.SetFloat("Speed", animSpeed);  //스크립트에 있는 animSpeed의 속도로 애니메이션이 진행되도록 설정
        /*
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
        }*/
        
    }
    protected virtual void FixedUpdate()
    {
        DistanceCheck();
        Move();
        AttackedCheck();
    }
    public void DistanceCheck() 
    //플레이어와 거리가 tracingDistance 이하일 때 추적하는 함수
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > tracingDistance)
        {
            isTracing = false;
            moveSpeed = 2.0f;
        }
        else
        {
            isTracing = true;
            moveSpeed = 4.0f;
        }
    }
    public void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (isTracing)  //추적중일 때는 기존 움직임을 멈추고 플레이어를 따라가도록 설정
        {
            //플레이어와의 수평거리와 수직거리 중 더 먼 것을 우선방향으로 설정하여 이동
            Vector3 playerPos = player.transform.position;
            float hori = Mathf.Abs(transform.position.x - playerPos.x);
            float vert = Mathf.Abs(transform.position.y - playerPos.y);
            if (hori<vert)
            {
                if (transform.position.y > playerPos.y) dist = "Down";
                else if (transform.position.y < playerPos.y) dist = "Up";
            }
            else if (hori>=vert)
            {
                if (transform.position.x > playerPos.x) dist = "Left";
                else if (transform.position.x < playerPos.x) dist = "Right";
            }
        }
        else    //추적중이 아닐 때는 설정한 movementFlag에 따라 움직임 ex)12345 => 왼쪽-오른쪽-가만히-위-아래
        {
            if (movementFlag == 1)
            {
                dist = "Left";
            }
            else if (movementFlag == 2)
            {
                dist = "Right";
            }
            else if (movementFlag == 4)
            {
                dist = "Up";
            }
            else if (movementFlag == 5)
            {
                dist = "Down";
            }
            else
            {
                dist = "Idle";
            }
        }

        if(dist == "Left")
        {
            animator.SetBool("isMoving", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveVelocity = Vector3.left;
        }
        else if(dist == "Right")
        {
            animator.SetBool("isMoving", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
            moveVelocity = Vector3.right;
        }
        else if(dist == "Up")
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.up;
        }
        else if(dist == "Down")
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.down;
        }
        else if(dist == "Idle")
        {
            animator.SetBool("isMoving", false);
        }
        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }

    //공격받았을 때 사망 처리/또는 hp감소
    protected virtual void AttackedCheck()
    {
        if (hp <= 0 && attackCheck)
        {
            attackCheck = false;
            this.GetComponent<EnemyOBJ>().Dead();
        }
        
        else if (attackCheck)   //몬스터가 플레이어의 공격에 피격 시 무적시간 동안 스프라이트 색 변경
        {
            StartCoroutine("Blinking");
        }
        
    }

   

    protected IEnumerator ChangeMovement() //코루틴으로 movementFlag를 1.5초마다 계속하여 변경
    {
        /*
         * movementFlag == 1 왼쪽으로 이동
         * movementFlag == 2 오른쪽으로 이동
         * movementFlag == 4 위쪽으로 이동
         * movementFlag == 5 아래쪽으로 이동
         * 그 외 나머지 가만히
         */
        if (movementFlag == 5) movementFlag = 0;
        else
        {
            movementFlag++;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("ChangeMovement");
    }
    public void Attack()    //플레이어 스크립트에서 Enemy 공격 시 호출할 함수
    {
        if (!isInvincible)
        {
            attackCheck = true;
            hp = hp - damaged;
        }

        if(hp>0)    StartCoroutine(enemyAttack());
    }

    //플레이어에게 공격받을 때 실행될 코루틴
    protected IEnumerator enemyAttack()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        attackCheck = false;
    }

    //설정된 시간 동안 무적을 적용하는 코루틴
    protected IEnumerator Blinking()
    {
        float countTime = 0;
        float blinkTic = 0.5f;
        if (invincibleTime < blinkTic) blinkTic = invincibleTime / 2.0f;

        while (countTime < invincibleTime)
        {
            if ((countTime / blinkTic) % 2 == 0)
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            else
                spriteRenderer.color = new Color(1, 1, 1, 0.8f);

            yield return new WaitForSeconds(blinkTic);

            countTime += blinkTic;
            //Debug.Log(countTime);
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        yield return null;
    }

}
