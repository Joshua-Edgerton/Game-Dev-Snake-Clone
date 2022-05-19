using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Speed and Movement")]
    public int projectileSpeed = 30;
    [Space(1)]
    [Header("Type of Projectile")]
    public bool isSpike;

    void Update()
    {
        this.transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "SnakeBody")
        {
            Debug.Log("enemy projectile hit snake head or body");
            other.GetComponent<Snake>().DieThenChooseSpawn();
            Destroy(this.gameObject);
        }
        if (other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
