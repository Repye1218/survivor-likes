using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Settings")] 
    public float spawnTime = 0.2f;
    public float spawnRadius = 20f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        // 반지름 1인 원 안 랜덤한 점을 정규화 -> (0, 1), (-0.4, 0.9) 같은 랜덤한 방향 백터 산출
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        
        // 랜덤 방향 백터에 Radius를 곱해줌
        Vector3 spawnPos = transform.position + (Vector3)(randomDir * spawnRadius);
        
        PoolManager.Instance.SpawnFromPool("Enemy", spawnPos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
