using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UiManager uiManager;

    [Header("Game Settings")]
    public static bool isGameRunning = false;
    public float gameTimer = 0.00f;
    [Range(0.0f, 1.0f)] public float gameSpeed = 1.0f;
    public int playerScore = 0;


    void Awake()
    {
        isGameRunning = true;
    }

    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("UiManager on Game Manager is NULL.");
        }

        StartCoroutine(GameTimerRoutine());
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    IEnumerator GameTimerRoutine()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(0.01f);
            gameTimer += 0.01f;
        }
    }

    public void ScoreCounter()
    {
        playerScore += 100;
    }
}
