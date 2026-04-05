using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed;
    
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x=Input.GetAxisRaw("Horizontal");
        inputVector.y=Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVector=inputVector.normalized *Time.fixedDeltaTime * speed ;
        rigid.MovePosition(rigid.position + nextVector);
    }
    
    void LateUpdate()
    {
        // 현재 속도를 애니메이터에 전달
        anim.SetFloat("Speed",inputVector.magnitude);
        
        // 키보드 입력이 있을 때만 마지막 방향을 갱신
        if (inputVector.x != 0 || inputVector.y != 0)
        {
            anim.SetFloat("LastDirX",inputVector.x);
            anim.SetFloat("LastDirY",inputVector.y);
            
        }
        // 현재 걷고 있는 방향 전달
        anim.SetFloat("DirX",inputVector.x);
        anim.SetFloat("DirY",inputVector.y);
        
        // 좌우 반전 처리
        if (inputVector.x != 0)
        {
            
            spriter.flipX = inputVector.x < 0;
        }
    }
}
