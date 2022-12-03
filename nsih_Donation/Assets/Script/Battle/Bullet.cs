using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject player;
    public float Speed = 5;
    public float rotateSpeed = 10;
    Vector3 dist;
    Vector3 dir;
    PlayerCon playerCon;

    // Start is called before the first frame update
    void OnEnable()
    {
        player = GameObject.Find("Player");
        dist = player.transform.position - gameObject.transform.position;
        dir = dist.normalized;
        Invoke("DestroyBullet", 3);
        AngleSet();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += dir * Speed * Time.deltaTime;
    }

    void AngleSet()
    {
        if (player != null)
        {
            Vector2 direction = new Vector2(
                transform.position.x - player.transform.position.x,
                transform.position.y - player.transform.position.y
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion angleAxis = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
            //Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.tag);
        if(collider.gameObject.tag == "Player")
        {
            playerCon = collider.GetComponent<PlayerCon>();
            if(playerCon.attackedCheck == false)
            {
                DestroyBullet();
                playerCon.minusHP(500);
                playerCon.attackedCheck = true;
                playerCon.StartCoroutine(playerCon.attacked());
            }
        }
    }
    

    void DestroyBullet()
    {
        this.gameObject.SetActive(false);
    }
}
