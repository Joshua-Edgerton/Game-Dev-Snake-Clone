using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int projectileSpeed = 30;
    public bool isSpike;

    private void Start()
    {

    }
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
