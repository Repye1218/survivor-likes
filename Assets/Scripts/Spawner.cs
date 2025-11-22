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
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        
        Vector3 spawnPos = transform.position + (Vector3)(randomDir * spawnRadius);
        
        PoolManager.Instance.SpawnFromPool("Enemy", spawnPos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
