using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public static Spawner main;
    [SerializeField] List<GameObject> enemyPrefabs; // Enemy Prefabs
    [SerializeField] float spawnRadius = 20f; // Spawn radius around player
    [SerializeField] float initialSpawnDelay = 10f; // Delay before first spawn
    [SerializeField] float spawnInterval = 5f; // Interval between waves
    [SerializeField] int maxEnemy = 45;
    [SerializeField] public GameObject followerPrefab;
    [SerializeField] public int maxEXP = 2;
    
	[HideInInspector] public int currentLevel = 1;
	[HideInInspector] public int currentEXP = 0;

    Transform player;
    int currentWave = 0;
    int enemiesAlive = 0;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemyWave), initialSpawnDelay, spawnInterval);
    }

    void SpawnEnemyWave()
    {
        // Increase the number of enemies with each wave
        int enemiesToSpawn = Mathf.Min(5 + currentWave / 2, 10);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnRandomEnemy();
        }

        currentWave++;
    }

    void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0) return;

        // Depending on the wave, the difficulty of enemies increases
        int enemyIndex = Mathf.Min(currentWave / 5, enemyPrefabs.Count - 1);
        GameObject enemyPrefab = enemyPrefabs[enemyIndex];

        if (enemiesAlive < maxEnemy)
        {
            Vector3 spawnPos = GetRandomNavMeshPosition(spawnRadius);

            if (spawnPos != Vector3.zero)
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                enemiesAlive++;
            }            
        }        
    }

    Vector3 GetRandomNavMeshPosition(float radius)
    {
        Vector3 randomPos = GameObject.FindGameObjectWithTag("Player").transform.position + Random.insideUnitSphere * radius;

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;
    }
}
