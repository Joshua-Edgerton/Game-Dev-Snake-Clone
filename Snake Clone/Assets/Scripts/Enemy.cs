using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Bounds")]
    public BoxCollider2D enemyArea;
    [Space(1)]
    [Header("Script Links")]
    public Spawner spawnerScript;
    public StatsManager statsManagerScript;
    [Space(1)]
    [Header("Health/Stats/Exp")]
    public int currentHealth;
    public int enemyHealthMax;
    public int expToGive;
    public int scoreToGive;
    [Space(1)]
    [Header("Game Objects")]
    public HealthBar healthBar;

    void Start()
    {
        //Script links
        statsManagerScript = GameObject.Find("Level Manager").GetComponent<StatsManager>();
        spawnerScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        //When a gameobject with this script attached to it spawns, it adds another count to the currentEnemy variable on the spawn script
        //This is for controlling the max amount of enemies to spawn
        spawnerScript.currentEnemyCount = spawnerScript.currentEnemyCount += 1;
        //Sets the current health to be the max health when starting - for the enemy and for the healthbar after
        currentHealth = enemyHealthMax;
        healthBar.SetMaxHealth(enemyHealthMax);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void DamageEnemy(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
        spawnerScript.currentEnemyCount -= 1;
        statsManagerScript.KillExp(expToGive);
        statsManagerScript.KillScore(scoreToGive);
    }

    public void Heal(int healAmount)
    {
        if (currentHealth < enemyHealthMax - healAmount)
        {
            currentHealth += healAmount;
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            currentHealth = enemyHealthMax;
            healthBar.SetHealth(currentHealth);
        }
    }
}
