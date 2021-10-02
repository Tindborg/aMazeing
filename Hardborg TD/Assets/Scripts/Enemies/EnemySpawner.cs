using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Singleton

    public static EnemySpawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }
    }

    #endregion Singleton

    public GameObject EnemyPrefab;
    public Transform SpawnPoint;

    [ContextMenu("Spawn Enemy")]
    public void SpawnEnemy( GameObject prefab )
    {
        Instantiate(prefab, SpawnPoint.position, Quaternion.identity);
    }

    private void Start()
    {
        
    }

    public void StartWave( int enemyCount, GameObject prefab )
    {
        StartCoroutine(Coroutine_Spawn(enemyCount, 0.15f, prefab));
    }

    [ContextMenu("Start Wave")]
    public void StartWaveDebug()
    {
        StartCoroutine(Coroutine_Spawn(10, 1f, EnemyPrefab));
    }

    private IEnumerator Coroutine_Spawn( int enemyCount, float delay, GameObject prefab )
    {
        int spawnedCount = 0;
        while (spawnedCount < enemyCount)
        {
            SpawnEnemy( prefab );
            spawnedCount += 1;
            yield return new WaitForSeconds(delay);
        }
    }
}
