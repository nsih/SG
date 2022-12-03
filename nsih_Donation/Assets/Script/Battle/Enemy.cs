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
    public bool attackCheck = true; //���� �÷��̾�� ���ݹ޾Ҵ��� Ȯ���ϴ� ����
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
        animator.SetFloat("Speed", animSpeed);  //��ũ��Ʈ�� �ִ� animSpeed�� �ӵ��� �ִϸ��̼��� ����ǵ��� ����
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
    //�÷��̾�� �Ÿ��� tracingDistance ������ �� �����ϴ� �Լ�
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

        if (isTracing)  //�������� ���� ���� �������� ���߰� �÷��̾ ���󰡵��� ����
        {
            //�÷��̾���� ����Ÿ��� �����Ÿ� �� �� �� ���� �켱�������� �����Ͽ� �̵�
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
        else    //�������� �ƴ� ���� ������ movementFlag�� ���� ������ ex)12345 => ����-������-������-��-�Ʒ�
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

    //���ݹ޾��� �� ��� ó��/�Ǵ� hp����
    protected virtual void AttackedCheck()
    {
        if (hp <= 0 && attackCheck)
        {
            attackCheck = false;
            this.GetComponent<EnemyOBJ>().Dead();
        }
        
        else if (attackCheck)   //���Ͱ� �÷��̾��� ���ݿ� �ǰ� �� �����ð� ���� ��������Ʈ �� ����
        {
            StartCoroutine("Blinking");
        }
        
    }

   

    protected IEnumerator ChangeMovement() //�ڷ�ƾ���� movementFlag�� 1.5�ʸ��� ����Ͽ� ����
    {
        /*
         * movementFlag == 1 �������� �̵�
         * movementFlag == 2 ���������� �̵�
         * movementFlag == 4 �������� �̵�
         * movementFlag == 5 �Ʒ������� �̵�
         * �� �� ������ ������
         */
        if (movementFlag == 5) movementFlag = 0;
        else
        {
            movementFlag++;
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("ChangeMovement");
    }
    public void Attack()    //�÷��̾� ��ũ��Ʈ���� Enemy ���� �� ȣ���� �Լ�
    {
        if (!isInvincible)
        {
            attackCheck = true;
            hp = hp - damaged;
        }

        if(hp>0)    StartCoroutine(enemyAttack());
    }

    //�÷��̾�� ���ݹ��� �� ����� �ڷ�ƾ
    protected IEnumerator enemyAttack()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
        attackCheck = false;
    }

    //������ �ð� ���� ������ �����ϴ� �ڷ�ƾ
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
