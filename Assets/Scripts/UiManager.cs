using System.Collections;
using TMPro;
using UnityEngine;


public class UiManager : MonoBehaviour
{
    [Header("Script Components")]
    GameManager gameManager;
    [SerializeField] private PlayerController player;
    [SerializeField] private Health playerHealthScript;
    [SerializeField] private Weapons weaponScript;
    [SerializeField] protected GunItem gunItem;

    [Header("TextMesh Components")]
    [SerializeField] private TextMeshProUGUI gameTimerTxt;
    [SerializeField] TextMeshProUGUI playerScoreTxt;
    [SerializeField] private TextMeshProUGUI playerHealthTxt;
    [SerializeField] private TextMeshProUGUI playerAmmoTxt;
    [SerializeField] private TextMeshProUGUI reloadingTxt;
    [SerializeField] private TextMeshProUGUI gameOverTxt;

    [Header("Settings")]
    [SerializeField] private float reloadTimeDelay;
    private float timer;


    void Start()
    {
        gameTimerTxt = GameObject.Find("Game_Timer_txt").GetComponent<TextMeshProUGUI>();
        if (gameTimerTxt == null)
        {
            Debug.LogError("GameTimer_txt on Canvas is NULL.");
        }

        playerScoreTxt = GameObject.Find("Player_Score_txt").GetComponent<TextMeshProUGUI>();
        if (playerScoreTxt == null)
        {
            Debug.LogError("Player_Score_txt on Canvas is NULL.");
        }

        playerHealthTxt = GameObject.Find("Player_Health_txt").GetComponent<TextMeshProUGUI>();
        if (playerHealthTxt == null)
        {
            Debug.LogError("Player_Health_txt on Canvas is NULL.");
        }

        playerAmmoTxt = GameObject.Find("Player_Ammo_txt").GetComponent<TextMeshProUGUI>();
        if (playerAmmoTxt == null)
        {
            Debug.LogError("Player_Ammo_txt on Canvas is NULL.");
        }

        reloadingTxt = GameObject.Find("Reloading_txt").GetComponent<TextMeshProUGUI>();
        if (reloadingTxt == null)
        {
            Debug.LogError("Reloading_txt on Canvas is NULL.");
        }

        gameOverTxt = GameObject.Find("Game_Over_txt").GetComponent<TextMeshProUGUI>();
        if (gameOverTxt == null)
        {
            Debug.LogError("Game_Over_txt on Canvas is NULL.");
        }

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("The GameManager Script on Canvas is NULL.");
        }

        playerHealthScript = GameObject.Find("Player").GetComponent<Health>();
        if (playerHealthScript == null)
        {
            Debug.LogError("The Player Health Script on Canvas is NULL.");
        }

        weaponScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<Weapons>();
        if (weaponScript == null)
        {
            Debug.LogError("The Weapon Script on Canvas is NULL.");
        }
    }

    void Update()
    {
        UpdateGameTimerDisplay();

        UpdateHealthDisplay();

        UpdateAmmoDisplay();

        timer += Time.deltaTime;
    }

    public void UpdateGameTimerDisplay()
    {
        gameTimerTxt.text = gameManager.gameTimer.ToString("0.00");
    }

    public void UpdateScoreDisplay()
    {
        playerScoreTxt.text = $"Score: " + gameManager.currentScore.ToString();
    }

    public void UpdateHealthDisplay()
    {
        playerHealthTxt.text = $"Health: {playerHealthScript.currentHealth}";
    }

    public void UpdateAmmoDisplay()
    {
        playerAmmoTxt.text = $"{player.currentBulletsLeft} / {player.currentMagazine}";
    }

    public IEnumerator ReloadingUIRoutine()
    {
        timer = 0;

        reloadTimeDelay = player.currentReloadTime / 3f;

        while (player.isReloading && timer <= player.currentReloadTime)
        {
            reloadingTxt.color = Color.white;
            reloadingTxt.text = $"reloading.  ";
            yield return new WaitForSeconds(reloadTimeDelay);
            reloadingTxt.text = $"reloading.. ";
            yield return new WaitForSeconds(reloadTimeDelay);
            reloadingTxt.text = $"reloading...";
            yield return new WaitForSeconds(reloadTimeDelay);
            reloadingTxt.color = Color.clear;
        }
        UpdateAmmoDisplay();
    }

    public IEnumerator GameOverRoutine()
    {
        while (GameManager.isPlayerDead)
        {
            gameOverTxt.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            gameOverTxt.color = Color.clear;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
