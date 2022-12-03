using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleGet : MonoBehaviour
{
    SpriteRenderer sprite;
    public GameObject rbButton;
    public bool isGet;

    void Start()
    {
        rbButton = GameObject.Find("Invincible");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isGet = true;
            rbButton.SetActive(true);
            Invoke("Destroy", 0.01f);
            Debug.Log("Get");
        }

    }

    void Destroy()
    {
        this.gameObject.SetActive(false);
    }

}
