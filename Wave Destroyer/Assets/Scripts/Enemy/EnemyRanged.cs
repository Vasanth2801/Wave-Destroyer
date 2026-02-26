using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    [Header("Chasing Settings")]
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 0.0025f;
    [SerializeField] private float bulletSpeed = 10f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    ObjectPooler pooler;

    [Header("Distance for the Enemy to Shoot")]
    [SerializeField] private float distanceToShoot = 5f;
    [SerializeField] private float distanceToStop = 2f;

    [Header("Firing Rate for the Enemy")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float timer = 0;

    [Header("References for the bullet and Enemy")]
    [SerializeField] private Transform firePoint;

    private void Awake()
    {
        timer = fireRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pooler = FindObjectOfType<ObjectPooler>();
    }

    private void Update()
    {
        if(player != null)
        {
            RotateTowardsTarget();
        }

        if (Vector2.Distance(transform.position, player.position) <= distanceToShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(timer < Time.time)
        {
           GameObject enemyBullet = pooler.SpawnFromPools("EnemyBullet", firePoint.position, firePoint.rotation);
           Rigidbody2D bulletRb = enemyBullet.GetComponent<Rigidbody2D>();
           bulletRb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
            timer = Time.time + fireRate;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position,player.position) >= distanceToStop)
        {
            rb.linearVelocity = transform.up * enemySpeed * Time.deltaTime;
        }
        else
        {
           rb.linearVelocity = Vector2.zero;
        }
    }

    void RotateTowardsTarget()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed);
    }
}