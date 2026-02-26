using UnityEngine;

public class Bullet : MonoBehaviour
{
    EnemyHealth health;

    void Start()
    {
        health = FindObjectOfType<EnemyHealth>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
           collision.GetContact(0).collider.GetComponent<EnemyHealth>().TakeDamage(10);
            gameObject.SetActive(false);
        }
       gameObject.SetActive(false);
    }
}