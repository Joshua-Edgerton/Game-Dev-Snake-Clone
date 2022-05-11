using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileSpeed = 30;
    private int abilityDamage = 1;
    public bool isVenomball;
    public int venomballDamage = 3;

    private void Start()
    {
        if (isVenomball)
        {
            abilityDamage = venomballDamage;
        }

    }
    void Update()
    {
        this.transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().DamageEnemy(abilityDamage);
            Destroy(gameObject);
        }
        if (other.tag == "Obstacle")
        {
            Debug.Log("touched obstacle");
            Destroy(gameObject);
        }
    }

}
