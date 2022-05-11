using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : MonoBehaviour
{
    public BoxCollider2D enemyArea;
    private Vector2 _direction = Vector2.left;
    public float enemySpeed = 0.1f;
    public float randomDirectionTimer = 3;
    public int randomDirectionChoice = 1;
    public Enemy enemyScript;
    public int turtleHealth = 30;
    public int amountHealedForFood = 10;
    public int amountHealedForSuperFood = 20;
    public Spawner spawnScript;
    void Start()
    {
        spawnScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        //Coroutine for custom update speedS
        StartCoroutine(TurtleUpdate());
        enemyScript = this.GetComponent<Enemy>();
        enemyScript.enemyHealthMax = turtleHealth;
        int randomDirectionStart = Random.Range(0, 5);
        if (randomDirectionStart == 1)
        {
            TurnUp();
        }
        else if (randomDirectionStart == 2)
        {
            TurnLeft();
        }
        else if (randomDirectionStart == 3)
        {
            TurnUp();
        }
        else if (randomDirectionStart == 4)
        {
            TurnDownForWhat();
        }

    }
    void Update()
    {

    }

    public void RandomDirection()
    {
        if (_direction == Vector2.up || _direction == Vector2.down)
        {
            if (randomDirectionChoice == 1)
            {
                TurnRight();
            }
            else if (randomDirectionChoice == 2)
            {
                TurnLeft();
            }
        }
        else if (_direction == Vector2.left || _direction == Vector2.right)
        {
            if (randomDirectionChoice == 1)
            {
                TurnUp();
            }
            else if (randomDirectionChoice == 2)
            {
                TurnDownForWhat();
            }
            randomDirectionChoice = 0;
        }
    }
    //Ienumerator that controls update speed for anything within "while" loop
    IEnumerator TurtleUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpeed);
            this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
        }
    }
    //The games default update speed
    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerProjectile")
        {

        }
        if (other.tag == "Obstacle" || other.tag == "Enemy" || other.tag == "Enemy Obstacle")
        {
            randomDirectionTimer = Random.Range(4f, 11f);
            randomDirectionChoice = Random.Range(1, 3);
            Invoke("RandomDirection", randomDirectionTimer);

            if (_direction == Vector2.left)
            {
                TurnRight();
            }
            else if (_direction == Vector2.right)
            {
                TurnLeft();
            }
            else if (_direction == Vector2.up)
            {
                TurnDownForWhat();
            }
            else if (_direction == Vector2.down)
            {
                TurnUp();
            }
        }
        if (other.tag == "Food")
        {
            enemyScript.Heal(amountHealedForFood);
        }
        if (other.tag == "SuperFood")
        {
            enemyScript.Heal(amountHealedForSuperFood);
        }
        if (other.tag == "Bounds")
        {
            Destroy(gameObject);
            if (spawnScript.currentEnemyCount! >= spawnScript.maxEnemies)
            {
                spawnScript.FirstSpawn();
            }
        }
    }

    private void TurnRight()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        _direction = Vector2.right;
    }
    private void TurnLeft()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _direction = Vector2.left;
    }
    private void TurnUp()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        _direction = Vector2.up;
    }
    private void TurnDownForWhat()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        _direction = Vector2.down;
    }
}
