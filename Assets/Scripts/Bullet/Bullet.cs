using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // 관통 데미지

    public void Init(float damage,int per)
    {
        this.damage = damage;
        this.per = per;
    }
}
