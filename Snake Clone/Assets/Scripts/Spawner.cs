using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Script Links")]
    public StatsManager statsManagerScript;
    [Space(1)]
    [Header("Bounds")]
    public BoxCollider2D spawnArea;
    [Space(1)]
    [Header("Lists")]
    public List<GameObject> _enemies = new List<GameObject>();
    [Space(1)]
    [Header("Enemy Control/Spawning")]
    public int maxEnemies = 2;
    public int currentEnemyCount = 0;
    public int enemySpawnTimer = 10;
    public Vector3 randomSpawnPoint;
    private float countDownTimer;
    public float spawnWarningTime = 2;
    public float spawnCountdownUI;
    [Space(1)]
    [Header("Particle Effects")]
    public GameObject spawnStarting;

    void Start()
    {
        Invoke("FirstSpawn", 1f);
        countDownTimer = enemySpawnTimer;
        spawnCountdownUI = enemySpawnTimer;
        statsManagerScript = GameObject.Find("Level Manager").GetComponent<StatsManager>();
    }

    void Update()
    {
        if (currentEnemyCount < maxEnemies)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer >= 0 && countDownTimer < enemySpawnTimer)
            {
                spawnCountdownUI = Mathf.Round(countDownTimer);
                statsManagerScript.spawnCounterUI.SetActive(true);
            }
            if (countDownTimer <= 0)
            {
                EnemySpawn();
                countDownTimer = enemySpawnTimer + spawnWarningTime;
                statsManagerScript.spawnCounterUI.SetActive(false);
            }
        }
        statsManagerScript.spawnCounter.text = spawnCountdownUI.ToString();
    }
    private void RandomSpawnGenerator()
    {
        Bounds bounds = this.spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        randomSpawnPoint = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }


    public void EnemySpawn()
    {
        if (currentEnemyCount < maxEnemies)
        {
            int randomEnemy = Random.Range(0, _enemies.Count);
            RandomSpawnGenerator();
            Instantiate(spawnStarting, randomSpawnPoint, this.transform.rotation);
            Invoke("Spawn", spawnWarningTime);
        }

    }
    public void FirstSpawn()
    {
        RandomSpawnGenerator();
        Instantiate(spawnStarting, randomSpawnPoint, this.transform.rotation);
        Invoke("Spawn", spawnWarningTime);
    }
    private void Spawn()
    {
        int randomEnemy = Random.Range(0, _enemies.Count);
        Instantiate(_enemies[randomEnemy], randomSpawnPoint, this.transform.rotation);
    }
}
