using System;
using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UiManager uiManager;
    [SerializeField] private SpawnManager spawnManager;

    [Header("Game Settings")]
    public static bool isGameRunning = false;
    public static bool isPlayerDead;
    public float gameTimer { get; private set; }
    [Range(0.0f, 1.0f)] public float gameSpeed = 1.0f;
    public int currentScore { get; private set; }
    public int pointsAwarded;


    [Header("Events")]
    public UnityEvent playerIsDead;
    public UnityEvent onEnemyDeath;


    void Awake()
    {
        isGameRunning = true;
        isPlayerDead = false;
    }

    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("UiManager on Game Manager is NULL.");
        }

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("SpawnManager on Game Manager is NULL.");
        }

        StartCoroutine(GameTimerRoutine());
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
        
        IncreaseDifficulty();
    }

    void IncreaseDifficulty()
    {
        switch (gameTimer)
        {
            case < 20f and >= 10f:
                spawnManager.enemySpawnDelay = 4.5f;
                break;
            case < 30f and >= 20f:
                spawnManager.enemySpawnDelay = 4f;
                break;
            case < 40f and >= 30f:
                spawnManager.enemySpawnDelay = 3.5f;
                break;
            case < 50f and >= 40f:
                spawnManager.enemySpawnDelay = 3f;
                break;
            case < 60f and >= 50f:
                spawnManager.enemySpawnDelay = 2.5f;
                break;
            case < 70f and >= 60f:
                spawnManager.enemySpawnDelay = 2f;
                break;
            case < 80f and >= 70f:
                spawnManager.enemySpawnDelay = 1.5f;
                break;
            case < 90f and >= 80f:
                spawnManager.enemySpawnDelay = 1f;
                break;
        }
    }

    public void IncrementScore()
    {
        currentScore += pointsAwarded;
    }

    public void PlayerIsDead()
    {
        isPlayerDead = true;
        StartCoroutine(uiManager.GameOverRoutine());
    }

    IEnumerator GameTimerRoutine()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(0.01f);
            gameTimer += 0.01f;
        }
    }
}
