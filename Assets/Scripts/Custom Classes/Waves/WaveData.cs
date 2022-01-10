using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct WaveData
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public float spawnRate;
    public bool hasPowerup;
    public bool spawnWithNoEnemies;
    public bool bossFight;

    [Header("Specific Pattern Settings")]
    public bool playSpecificPattern;
    public int specificPattern;
}
