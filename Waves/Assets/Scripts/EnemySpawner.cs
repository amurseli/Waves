using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to your Enemy prefab
    public float initialSpawnInterval = 5f; // Time interval between spawns
    public float spawnRange = 5.0f;

    public List<Wave> waves;
    private int currentWaveIndex = 0;
    
    public float spawnIntervalIncreaseRate = 0.1f; // Rate at which spawn interval increases

    private float currentSpawnInterval; // Current time interval between spawns

    
    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];
            Debug.Log("Wave Numero" + (currentWaveIndex + 1));


            float spawnInterval = currentWave.spawnInterval;
            float growthRate = currentWave.growthRate;
            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                float newspawnInterval = spawnInterval - growthRate; 
                yield return new WaitForSeconds(newspawnInterval);
                spawnInterval = newspawnInterval;
                // Spawn a new enemy
                SpawnEnemy();
                Debug.Log("Enemies Remaining: " + (currentWave.enemyCount - (i + 1)));
            }
            //Aca iría el codigo para cuando se termina una Wave

            // Move to the next wave
            currentWaveIndex++;
        }
    }

    void SpawnEnemy()
    {

        float offsetX = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    }
}
