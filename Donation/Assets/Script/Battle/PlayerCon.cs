using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    GameObject playerManager;
    GameObject player;
    GameObject swordRad;
    GameObject sword;
    GameObject attackE;

    Collider2D attackECollider;




    public float timerTime;
    public bool isAttackCool;
    public static float attackSpeed;
    
    void Awake()
    {
        playerManager = GameObject.Find("PlayerManager");
        //sword = GameObject.Find("attackE");
        player = GameObject.Find("Player");
        swordRad = GameObject.Find("SwordRotate");
        attackE = swordRad.transform.GetChild(0).gameObject;//attackE

        attackECollider = attackE.GetComponent<Collider2D>();

        attackSpeed = 1.0f;
        isAttackCool = false;
    }

    void Start()  
    {
        attackE.SetActive(false);
    }

    void Update() 
    {
        AsTimer();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        AttackAim();
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









    ///. attack


    Vector2 _mousePos;
    Vector2 _playerPos;

    public void AttackAim()
    {
        _mousePos = Input.mousePosition;
        _playerPos = player.gameObject.transform.position;

        Vector3 target = Camera.main.ScreenToWorldPoint(_mousePos);

        float dy = target.y - _playerPos.y;
        float dx = target.x - _playerPos.x;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        swordRad.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
    }

    public void Attack()
    {
        if (Input.GetMouseButton(0) && !isAttackCool )
        {
            isAttackCool = true;
            StartCoroutine(ActiveAttack());
        }
    }

    IEnumerator ActiveAttack()
    {
        attackE.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        attackE.SetActive(false);
    }
    
    void AsTimer()
    {
        if(timerTime < (1/attackSpeed) )
        {
            timerTime += Time.deltaTime;
        }
            
        else
        {
            isAttackCool = false;            
            timerTime = 0;
        }
    }
}
