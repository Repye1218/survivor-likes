using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    private Vector3 _direction;

    public void Init(Vector3 dir)
    {
        _direction = dir.normalized; // 방향 정규화
        
        // 발사될 때 각도를 적 방향으로 회전
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnEnable()
    {
        // 풀에서 나올때 마다 타이머 리셋
        CancelInvoke();
        Invoke("DisableBullet", lifeTime);
    }

    public void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime), Space.World);
    }
    
    void DisableBullet() 
    {
        PoolManager.Instance.ReturnToPool("Bullet", gameObject);
    }

    void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.CompareTag("Enemy") || collsion.CompareTag("Wall"))
        {
            // 데미지 로직 추가할 부분
            Debug.Log("Enemy Hit");
            DisableBullet();
        }
    }
}
