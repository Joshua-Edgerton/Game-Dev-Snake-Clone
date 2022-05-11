using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealthMax;
    public int currentHealth;
    public BoxCollider2D enemyArea;
    public Spawner spawnerScript;
    public HealthBar healthBar;
    void Start()
    {
        spawnerScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        spawnerScript.currentEnemyCount = spawnerScript.currentEnemyCount += 1;
        Debug.Log(spawnerScript.currentEnemyCount + " current enemies");
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
        }
    }
}
