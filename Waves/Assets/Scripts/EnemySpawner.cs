using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to your Enemy prefab
    public List<GameObject> specialEnemyList;
    public float initialSpawnInterval = 5f; // Time interval between spawns
    public float spawnRange = 5.0f;
    public int multipleEnemies = 0;
    
    public TextMeshPro waveText;

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
            waveText.text = (currentWaveIndex + 1).ToString();
            yield return new WaitForSeconds(5f);
            waveText.text = "";
            Debug.Log("Wave Numero" + (currentWaveIndex + 1));
            multipleEnemies = 0;

            float spawnInterval = currentWave.spawnInterval;
            float growthRate = currentWave.growthRate;
            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                float newspawnInterval = spawnInterval - growthRate; 
                yield return new WaitForSeconds(newspawnInterval);
                spawnInterval = newspawnInterval;
                // Spawn a new enemy
                SpawnEnemy(i);
                Debug.Log("Enemies Remaining: " + (currentWave.enemyCount - (i + 1)));
            }
            //Aca iría el codigo para cuando se termina una Wave

            // Move to the next wave
            currentWaveIndex++;
        }
    }

    void SpawnEnemy(int enemyNumber)
    {
        multipleEnemies++;
        float offsetX = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        if (Random.Range(0, 20) < (enemyNumber * 0.18f) && multipleEnemies <= 3 && enemyNumber > 3)
        { 
            SpawnEnemy(enemyNumber);
            multipleEnemies = 0;
        }

        if (Random.Range(0, 20) < (enemyNumber * 2.15f))
        {
            GameObject specialEnemy = Instantiate(specialEnemyList[Random.Range(0,5)], getSimilarSpawnPosition(spawnPosition), Quaternion.identity);
        }
    }

    Vector3 getSimilarSpawnPosition(Vector3 initialSpawn)
    {
        return new Vector3(initialSpawn.x + Random.Range(1, 2), initialSpawn.y + Random.Range(1, 2), initialSpawn.z);
    }
}
