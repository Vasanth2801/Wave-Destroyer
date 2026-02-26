using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    PlayerHealth health;

    void Start()
    {
        health = FindObjectOfType<PlayerHealth>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(10);
            gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}