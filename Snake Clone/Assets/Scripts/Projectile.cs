using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Speed and Movement")]
    [Range(1, 100)]
    public int projectileSpeed = 30;
    [Space(1)]
    [Header("General Ability Variables")]
    private int abilityDamage = 1;
    [Space(1)]
    [Header("Projectile Type/Damage")]
    public bool isVenomball;
    [Range(1, 50)]
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
            Destroy(gameObject);
        }
    }

}
