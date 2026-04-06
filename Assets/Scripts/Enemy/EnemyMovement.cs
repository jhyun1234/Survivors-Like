using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive=true; // 테스트용 true
    
    Rigidbody2D rigid;
    SpriteRenderer sr;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }
        Vector2 direction = target.position -rigid.position;
        Vector2 nextVector = direction.normalized * Time.fixedDeltaTime * speed;
        rigid.MovePosition(rigid.position + nextVector);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }
        sr.flipX = target.position.x < rigid.position.x;
    }
}
