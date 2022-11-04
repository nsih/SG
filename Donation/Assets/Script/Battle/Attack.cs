using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0.3f);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && sprite.color == new Color(1, 1, 1, 1))
        {
            collision.gameObject.GetComponent<Enemy>().Attack();
        }
    }


    public IEnumerator SwingSword()
    {
        sprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.3f);
        sprite.color = new Color(1, 1, 1, 0.3f);

        //오브젝트 새로고침
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }
}
