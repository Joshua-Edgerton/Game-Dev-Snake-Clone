using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileSpeed = 30;
    //public Rigidbody2D theRB;
    void Update()
    {
        this.transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
        //theRB.velocity = transform.right * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("touched obstacle");
            Destroy(gameObject);
        }
    }

}
