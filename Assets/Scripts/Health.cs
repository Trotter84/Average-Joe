using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] [Range(1, 5)] private int startingHealth = 5;
    public int currentHealth;


    void OnEnable()
    {
        currentHealth = startingHealth;
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
        gameObject.SetActive(false);
    }
}
