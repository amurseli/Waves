using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to your Enemy prefab
    public float spawnInterval = 5f; // Time interval between spawns
    public float spawnRange = 5.0f;
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

        float offsetX = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    }
}
