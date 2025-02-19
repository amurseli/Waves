using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to your Enemy prefab
    //public List<GameObject> specialEnemyList;
    public float initialSpawnInterval = 5f; // Time interval between spawns
    public float spawnRange = 5.0f;
    public int multipleEnemies = 0;
    
    public TextMeshPro waveText;

    public writer writer;

    public GameObject dialogueBox;

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
            bool waitForCondition = true;
            if (currentWave.hasScene())
            {
                Debug.Log(currentWave.hasScene());
                dialogueBox.GetComponent<Animator>().SetTrigger("Open");
            }
            currentWave.StartDialogue();
            writer.randomWordSelector(currentWave.WordsCsv);
            // Modify this loop condition based on your specific condition to wait
            while (waitForCondition)
            {
                // Check the condition
                waitForCondition = DialogueManager.onConversation;
                if (!waitForCondition)
                {
                    Debug.Log(waitForCondition);
                }
                // Wait for a short interval before checking again
                yield return null;
            }
            dialogueBox.GetComponent<Animator>().SetTrigger("Close");
            writer.canvasField.text = "";
            
            waveText.text = (currentWaveIndex + 1).ToString();
            yield return new WaitForSeconds(2f);
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
                SpawnEnemy(i, currentWave);
                Debug.Log("Enemies Remaining: " + (currentWave.enemyCount - (i + 1)));
            }

            // Check for the conditions to start the next wave
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            while (enemies.Length > 0)
            {
                Debug.Log(enemies.Length);
                yield return null;
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }
        
            //Aca iría el codigo para cuando se termina una Wave
            
            // Move to the next wave
            currentWaveIndex++;
        }
    }
    
    void SpawnEnemy(int enemyNumber, Wave currentWave)
    {
        multipleEnemies++;
        float offsetX = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z);

        if(!currentWave.shouldSpawnSpecial(enemyNumber)){
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
            if (Random.Range(0, 20) < (enemyNumber * 0.18f) && multipleEnemies <= 3 && enemyNumber > 3)
            { 
                SpawnEnemy(enemyNumber, currentWave);
                multipleEnemies += 1;
            }
        }

        if (currentWave.shouldSpawnSpecial(enemyNumber))
        {
            GameObject specialEnemy = Instantiate(currentWave.getSpecialEnemy(enemyNumber), getSimilarSpawnPosition(spawnPosition), Quaternion.identity);
        }
    }

    Vector3 getSimilarSpawnPosition(Vector3 initialSpawn)
    {
        return new Vector3(initialSpawn.x + Random.Range(1, 2), initialSpawn.y + Random.Range(1, 2), initialSpawn.z);
    }
}
