using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Berserker : Enemy
{
    Vector3 dist;
    Vector3 dir;
    
    // Start is called before the first frame update
   protected override void OnEnable()
    {
        base.OnEnable();
        //�߰��� ������ ����
        hp = 2;
        VecCheck();
        animSpeed = 0.8f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        animator.SetBool("isMoving", true);
        //�߰��� ������ ����
    }
    protected override void FixedUpdate()
    {
        Move();
        AttackedCheck();
    }
    
    public void VecCheck()
    {
        dist = player.transform.position - gameObject.transform.position;
        dir = dist.normalized;
    }
    new public void Move()
    {
        this.transform.position += dir * moveSpeed * Time.deltaTime;
    }
    
}
