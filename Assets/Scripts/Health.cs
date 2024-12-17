using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameManager gameManager;

    [Header("Attributes")]
    [SerializeField] [Range(1, 5)] private int startingHealth = 5;
    public int currentHealth;


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
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            gameManager.ScoreCounter();
        }
        gameObject.SetActive(false);
    }
}
