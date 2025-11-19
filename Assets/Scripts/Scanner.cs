using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [Header("Settings")] 
    public float scanRange = 5f;
    public float fireRate = 0.5f;
    public LayerMask targetLayer;

    private float timer;
    private Transform target;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            timer = 0;
            target = GetNearestTarget();

            if (target != null)
            {
                Fire();
            }
        }
    }

    Transform GetNearestTarget()
    {
        // scanRange 반경안 모든 적 찾기
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, scanRange, targetLayer);

        Transform nearestTarget = null;
        float diff = 100f; // 거리 비교용

        foreach (Collider2D targetCollider in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = targetCollider.transform.position;
            
            float curDiff = Vector3.Distance(myPos, targetPos);

            // 가장 가까운 곳으로 갱신
            if (curDiff < diff)
            {
                diff = curDiff;
                nearestTarget = targetCollider.transform;
            }
        }
        
        return nearestTarget;
    }

    void Fire()
    {
        Vector3 dir = target.position - transform.position;
        
        GameObject bulletObj = PoolManager.Instance.SpawnFromPool
            ("Bullet", transform.position, Quaternion.identity);

        if (bulletObj != null)
        {
            bulletObj.GetComponent<Bullet>().Init(dir);
        }
    }

    // 에디터에서 탐색 범위 확인코드
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}
