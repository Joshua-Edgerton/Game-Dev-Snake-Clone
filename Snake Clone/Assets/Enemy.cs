using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100;
    public BoxCollider2D enemyArea;
    public Spawner spawnerScript;
    void Start()
    {
        spawnerScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        spawnerScript.currentEnemyCount = spawnerScript.currentEnemyCount += 1;
        Debug.Log(spawnerScript.currentEnemyCount + " current enemies");
    }
    void Update()
    {

    }

    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        Debug.Log(enemyHealth);
    }
}
