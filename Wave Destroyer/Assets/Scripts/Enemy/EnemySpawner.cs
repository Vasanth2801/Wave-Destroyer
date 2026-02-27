using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public float timeBetweenSpawns = 0.5f;
        public float timeBetweenWaves = 1f;
        public int enemiesCount;
    }

    [Header("Wave Settings")]
    [SerializeField] private float countDown;
    public Wave[] waves;
    public Transform[] spawnPoint;
    public int currentWave = 0;
    public  bool countDownBegin;
    [SerializeField] private TextMeshProUGUI waveCountDownText;

    void Start()
    {
        countDownBegin = true;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesCount = waves[i].enemies.Length;
        }

        waveCountDownText.text = "Current Wave: " + currentWave.ToString();
    }

    void Update()
    {
        if (currentWave >= waves.Length)
        {
            Debug.Log("All Waves Completed!");
            return;
        }

        if (countDownBegin == true)
        {
            countDown -= Time.deltaTime;
        }

        if (countDown <= 0f)
        {
            countDownBegin = false;
            countDown = waves[currentWave].timeBetweenWaves;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWave].enemiesCount == 0)
        {
            countDownBegin = true;
            currentWave++;
        }
        waveCountDownText.text = "Current Wave: " + currentWave.ToString();
    }

    IEnumerator SpawnWave()
    {
        if (currentWave < waves.Length)
        {
            for (int i = 0; i < waves[currentWave].enemies.Length; i++)
            {
                Transform spawnPoints = spawnPoint[Random.Range(0, spawnPoint.Length)];
                GameObject enemy = Instantiate(waves[currentWave].enemies[i], spawnPoints.position, Quaternion.identity);
                enemy.transform.SetParent(spawnPoints);
                yield return new WaitForSeconds(waves[currentWave].timeBetweenSpawns);
            }
            Debug.Log("Wave " + currentWave + " Spawned!");
        }
    }
}