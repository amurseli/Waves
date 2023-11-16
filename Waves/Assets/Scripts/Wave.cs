using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public int enemyCount;
    public float spawnInterval;
    public float growthRate;
    
}