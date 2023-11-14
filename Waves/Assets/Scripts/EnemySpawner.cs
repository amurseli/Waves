using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to your Enemy prefab
    public float spawnInterval = 5f; // Time interval between spawns

    void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Spawn a new enemy
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Instantiate a new enemy at the spawner's position
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Optionally, you can perform additional setup for the new enemy
        // For example, set its initial position, configure behavior, etc.
    }
}
