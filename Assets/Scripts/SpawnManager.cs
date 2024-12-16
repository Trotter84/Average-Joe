using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Manager")]
    [SerializeField] private bool isSpawning;

    [Header("Enemy Pool")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyCountToPool = 10;
    private GameObject enemyContainer;
    [SerializeField] private List<GameObject> pooledEnemies = new List<GameObject>();

    [Header("Enemy Spawn Details")]
    [SerializeField] private float enemySpawnDelay = 4f;

    int posNegX;
    int posNegY;


    void Awake()
    {
        isSpawning = true;
    }

    void Start()
    {
        InitiatePools();

        StartCoroutine(SpawnEnemyRoutine());
    }

    private void InitiatePools()
    {
        for (int i = 0; i < enemyCountToPool; i++)
        {
            GameObject enemies = Instantiate(enemyPrefab);
            enemies.transform.parent = transform;
            enemies.SetActive(false);
            pooledEnemies.Add(enemies);
        }
    }

    public GameObject GetPooledEnemies()
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }
        return null;
    }

    void SpawnLocation()
    {
        int randX = Random.Range(0, 2);
        if (randX == 0)
        {
            posNegX = -1;
        }
        if (randX == 1)
        {
            posNegX = 1;
        }

        int randY = Random.Range(0, 2);
        if (randY == 0)
        {
            posNegY = -1;
        }
        if (randY == 1)
        {
            posNegY = 1;
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (isSpawning)
        {
            GameObject enemy = GetPooledEnemies();

            if (enemy != null)
            {
                SpawnLocation();
                enemy.transform.position = new Vector3(16 * posNegX, 9.5f * posNegY, -1);
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
}
