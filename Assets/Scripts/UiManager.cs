using System.Collections;
using TMPro;
using UnityEngine;


public class UiManager : MonoBehaviour
{
    [Header("Script Components")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Health playerHealthScript;
    [SerializeField] private Weapon weaponScript;

    [Header("TextMesh Components")]
    [SerializeField] private TextMeshProUGUI gameTimerTxt;
    [SerializeField] private TextMeshProUGUI playerScoreTxt;
    [SerializeField] private TextMeshProUGUI playerHealthTxt;
    [SerializeField] private TextMeshProUGUI playerAmmoTxt;
    [SerializeField] private TextMeshProUGUI reloadingTxt;

    [SerializeField] private float reloadTimeDelay;


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

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("The GameManager Script on Canvas is NULL.");
        }

        playerHealthScript = GameObject.Find("Player").GetComponent<Health>();
        if (playerHealthScript == null)
        {
            Debug.LogError("The Player Health Script on Canvas is NULL.");
        }

        weaponScript = GameObject.Find("Gun").GetComponent<Weapon>();
        if (weaponScript == null)
        {
            Debug.LogError("The Weapon Script on Canvas is NULL.");
        }
    }

    void Update()
    {
        UpdateGameTimerDisplay();

        UpdatePlayerScoreDisplay();

        UpdateHealthDisplay();

        UpdateAmmoDisplay();
    }

    void UpdateGameTimerDisplay()
    {
        gameTimerTxt.text = gameManager.gameTimer.ToString("0.00");
    }

    void UpdatePlayerScoreDisplay()
    {
        playerScoreTxt.text = $"Score: {gameManager.playerScore}";
    }

    void UpdateHealthDisplay()
    {
        playerHealthTxt.text = $"Health: {playerHealthScript.currentHealth}";
    }

    void UpdateAmmoDisplay()
    {
        playerAmmoTxt.text = $"{weaponScript.bulletsLeft} / {weaponScript.magazineSize}";
    }

    public IEnumerator ReloadingUIRoutine()
    {
        reloadTimeDelay = weaponScript.reloadTime / 3;

        Debug.LogWarning(reloadTimeDelay);

        while (weaponScript.isReloading)
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
}
