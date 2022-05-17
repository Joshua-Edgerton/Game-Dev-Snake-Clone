using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealthMax;
    public int currentHealth;
    public BoxCollider2D enemyArea;
    public Spawner spawnerScript;
    public StatsManager statsManagerScript;
    public HealthBar healthBar;
    public int expToGive;
    public int scoreToGive;
    void Start()
    {
        statsManagerScript = GameObject.Find("Level Manager").GetComponent<StatsManager>();
        spawnerScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        spawnerScript.currentEnemyCount = spawnerScript.currentEnemyCount += 1;
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
