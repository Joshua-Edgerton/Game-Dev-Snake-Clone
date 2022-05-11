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
    void Start()
    {
        StartCoroutine(EnemySpawn());
        Invoke("FirstSpawn", 1f);
    }

    void Update()
    {

    }
    private void RandomSpawnGenerator()
    {
        Bounds bounds = this.spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        randomSpawnPoint = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }


    IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnTimer);
            if (currentEnemyCount < maxEnemies)
            {
                int randomEnemy = Random.Range(0, _enemies.Count);
                RandomSpawnGenerator();
                Instantiate(spawnStarting, randomSpawnPoint, this.transform.rotation);
                Invoke("Spawn", 2f);
                //int enemyPicker = Random.Range(0, 100);
            }

        }
    }
    public void FirstSpawn()
    {
        RandomSpawnGenerator();
        Instantiate(spawnStarting, randomSpawnPoint, this.transform.rotation);
        Invoke("Spawn", 2f);
    }
    private void Spawn()
    {
        int randomEnemy = Random.Range(0, _enemies.Count);
        Instantiate(_enemies[randomEnemy], randomSpawnPoint, this.transform.rotation);
    }
}
