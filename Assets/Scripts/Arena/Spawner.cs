using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs; // Enemy Prefabs
    [SerializeField] float spawnRadius = 20f; // Spawn radius around player
    [SerializeField] float initialSpawnDelay = 2f; // Delay before first spawn
    [SerializeField] float spawnInterval = 5f; // Interval between waves

    Transform player;
    int currentWave = 0;
    int enemiesAlive = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemyWave), initialSpawnDelay, spawnInterval);
    }

    void SpawnEnemyWave()
    {
        // Increase the number of enemies with each wave
        int enemiesToSpawn = Mathf.Min(3 + currentWave / 2, 10);
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

        // Random position around the player
        Vector3 randomPos = GameObject.FindGameObjectWithTag("Player").transform.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = 0;

        if (enemiesAlive < 45)
        {
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            enemiesAlive++;
        }        
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;
    }
}
