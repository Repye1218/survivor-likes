using System;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed = 2.5f;
    public float health = 10f;
    
    private Rigidbody2D rb;
    private Transform target;
    private bool isLive;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isLive = true;
        health = 10f;
    }

    void FixedUpdate()
    {
        // 본인이 살아있거나 타겟이 없으면
        if (!isLive || target == null) return;
        
        Vector2 dirVector = target.position - transform.position;
        Vector2 nxtVector = dirVector.normalized * (speed * Time.fixedDeltaTime);
        
        // later: spiterander를 이용하기 위한 flipX 사용
        
        rb.MovePosition(rb.position + nxtVector);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 5f;
            collision.GetComponent<Bullet>().DisableBullet();

            if (health <= 0f)
            {
                Dead();
            }
        }        
    }

    void Dead()
    {
        isLive = false;
        PoolManager.Instance.ReturnToPool("Enemy", gameObject);
    }
}
