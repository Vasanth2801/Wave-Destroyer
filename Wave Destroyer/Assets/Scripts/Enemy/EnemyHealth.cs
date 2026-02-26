using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int currentHealth;
    [SerializeField] EnemySpawner spawner;

    void Start()
    {
        currentHealth = maxHealth;
        spawner = FindObjectOfType<EnemySpawner>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            spawner.waves[spawner.currentWave].enemiesCount--;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
