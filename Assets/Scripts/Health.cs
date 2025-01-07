using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UiManager uiManager;

    [Header("Attributes")]
    [SerializeField] [Range(1, 5)] private int startingHealth = 5;
    public float currentHealth;


    void OnEnable()
    {
        currentHealth = startingHealth;
    }

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("Game Manager on Health is NULL.");
        }

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("Canvas:UiManager on Health is NULL.");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        if (gameObject.CompareTag("Player"))
        {
            gameManager.playerIsDead.Invoke();
        }
        if (gameObject.CompareTag("Enemy"))
        {
            gameManager.onEnemyDeath.Invoke();
            print($"Enemy killed. + {gameManager.currentScore} points.");
        }
        gameObject.SetActive(false);
    }
}