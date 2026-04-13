using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적의 이동 및 스프라이트 반전을 담당하는 컨트롤러.
/// 매 물리 프레임마다 플레이어 위치를 추적하여 접근한다.
/// 오브젝트 풀링과 함께 사용되며, OnEnable에서 플레이어 타겟을 재설정한다.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    /// <summary>
    /// 적의 이동 속도. 인스펙터에서 적 종류별로 다르게 설정 가능.
    /// </summary>
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animControllers;

    /// <summary>
    /// 추적할 플레이어의 Rigidbody2D. OnEnable에서 자동으로 할당된다.
    /// </summary>
    public Rigidbody2D target;

    /// <summary>
    /// 적의 생존 여부. false이면 이동과 반전 처리를 모두 중단한다.
    /// </summary>
    bool isLive; // 테스트용 true

    Rigidbody2D rigid; // 물리 이동에 사용하는 Rigidbody2D 컴포넌트
    SpriteRenderer sr; // 좌우 반전 처리에 사용하는 SpriteRenderer 컴포넌트
    Animator anim; // 애니메이션 제어에 사용하는 Animator 컴포넌트

    void Awake()
    {
        // 컴포넌트 캐싱 (Awake에서 한 번만 가져와 성능 최적화)
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 사망 상태면 이동 처리 생략
        if (!isLive)
            return;

        // 플레이어 방향으로 이동 벡터 계산
        Vector2 direction = target.position - rigid.position;

        // normalized: 방향만 유지하고 크기를 1로 정규화 → 속도가 거리에 영향받지 않음
        Vector2 nextVector = direction.normalized * Time.fixedDeltaTime * speed;
        rigid.MovePosition(rigid.position + nextVector);

        // 관성 제거: MovePosition 후 남아있는 물리 속도를 0으로 초기화
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        // 사망 상태면 반전 처리 생략
        if (!isLive)
            return;

        // 플레이어가 적의 왼쪽에 있으면 스프라이트를 왼쪽으로 반전
        sr.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        // 풀에서 꺼내질 때마다 플레이어 타겟을 재탐색
        // 씬 재시작 또는 플레이어 오브젝트 변경 시에도 올바른 타겟을 참조하도록 보장
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animControllers[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            
        }
        else
        {
            Dead();
        }
        
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}