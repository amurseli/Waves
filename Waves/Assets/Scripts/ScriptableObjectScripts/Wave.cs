using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[SerializedDictionary("With Enemy", "Spawn")]
[CreateAssetMenu(fileName = "NewWave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public int enemyCount;
    public float spawnInterval;
    public float growthRate;
    
    public SerializedDictionary<int,GameObject> specialEnemyList;

    [SerializeField]
    public Dialogue dialogue;
    
    public bool shouldSpawnSpecial(int enemyNumber)
    {
        foreach (var enemy in specialEnemyList)
        {
            if (enemyNumber == enemy.Key)
            {
                return true;
            }
        }
        return false;
    }

    public void StartDialogue()
    {
        DialogueManager.StartDialogue(dialogue);
    }
    
    public GameObject getSpecialEnemy(int enemyNumber)
    {
        return specialEnemyList[enemyNumber];
        
    }
}