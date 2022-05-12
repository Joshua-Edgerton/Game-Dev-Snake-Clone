using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> _enemies = new List<GameObject>();
    public BoxCollider2D spawnArea;
    public int maxEnemies = 2;
    public int currentEnemyCount = 0;
    public int enemySpawnTimer = 10;
    public Vector3 randomSpawnPoint;
    public GameObject spawnStarting;
    private float countDownTimer;
    public float spawnWarningTime = 2;
    public float spawnCountdownUI;
    public StatsManager statsManagerScript;
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
                Debug.Log("countdown ui" + spawnCountdownUI);
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
            //int enemyPicker = Random.Range(0, 100);
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
