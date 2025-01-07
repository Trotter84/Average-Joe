using System.Collections;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject player;
    [SerializeField] private SpawnManager spawnManager;
    
    
    public Vector3 waypointPosition;
    private float timer;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player on Waypoint is NULL");
        }

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("Spawn Manager : SpawnManager on Waypoint is NULL");
        }
    }

    void Update()
    {
        if (!GameManager.isPlayerDead)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                waypointPosition = player.transform.position;
                timer = 0;
            }
        }
    }

    public void ChangeEnemyTarget()
    {
        StartCoroutine(MovePositionToRandom());
    }

    IEnumerator MovePositionToRandom()
    {
        while (GameManager.isPlayerDead)
        {
            foreach (var Enemy in spawnManager.pooledEnemies)
            {
                if (Enemy.activeInHierarchy)
                {
                    waypointPosition = new Vector3(Random.Range(-15f, 15f), Random.Range(-8f, 8f), -1);
                    yield return new WaitForSeconds(Random.Range(2f, 5f));
                }
            }
        }
    }

}
