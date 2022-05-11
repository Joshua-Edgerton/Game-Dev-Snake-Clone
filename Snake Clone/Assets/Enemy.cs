using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100;
    public BoxCollider2D enemyArea;
    void Start()
    {

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
