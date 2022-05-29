using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : MonoBehaviour
{
    [Header("Enemy Bounds")]
    public BoxCollider2D enemyArea;
    [Space(1)]
    [Header("Movement")]
    private Vector2 _direction = Vector2.left;
    public float enemySpeed = 0.1f;
    public float randomDirectionTimer = 3;
    public float randomDirectionDefault = 2;
    public float randomDirectionCounter;
    public int randomDirectionChoice = 1;
    public float triggerCounter;
    public float triggerTimer = 0.2f;
    [Space(1)]
    [Header("Script Links")]
    public Enemy enemyScript;
    public Spawner spawnScript;
    public HealthBar healthBarScript;
    [Space(1)]
    [Header("Health/Stats/EXP")]
    [Range(1, 100)]
    public int turtleHealth = 30;
    [Range(1, 100)]
    public int amountHealedForFood = 10;
    [Range(1, 100)]
    public int amountHealedForSuperFood = 20;
    [Range(1, 10)]
    public int expWorth = 2;
    [Range(100, 10000)]
    public int scoreWorth = 200;
    [Space(1)]
    [Header("Game Objects")]
    public GameObject healthBarContainer;
    void Start()
    {
        randomDirectionCounter = randomDirectionDefault;
        healthBarScript = healthBarContainer.GetComponent<HealthBar>();
        enemyScript.expToGive = expWorth;
        enemyScript.scoreToGive = scoreWorth;
        spawnScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
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
        LayerMask boundsMask = LayerMask.GetMask("Bounds");
        //Raycast that finds the object "Bounds" whenever it is in front of the enemy, and returns "true" if it is within range
        RaycastHit2D boundsInFront = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 4f, boundsMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 4f, Color.red);

        if (boundsInFront)
        {
            Debug.Log("Enemy saved by Raycast Hitting Bounds");
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

        if (randomDirectionCounter > 0)
        {
            randomDirectionCounter -= Time.deltaTime;
        }
        if (triggerCounter > 0)
        {
            triggerCounter -= Time.deltaTime;
        }
        if (_direction == Vector2.left || _direction == Vector2.right)
        {
            healthBarScript.isTurtleSideways = true;
        }
        else
        {
            healthBarScript.isTurtleSideways = false;
        }

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

            randomDirectionTimer = Random.Range(2f, 6f);
            randomDirectionChoice = Random.Range(1, 3);
            if (randomDirectionCounter <= 0)
            {
                Invoke("RandomDirection", randomDirectionTimer);
                randomDirectionCounter = randomDirectionDefault;
            }

            if (_direction == Vector2.left && triggerCounter <= 0)
            {
                TurnRight();
            }
            else if (_direction == Vector2.right && triggerCounter <= 0)
            {
                TurnLeft();
            }
            else if (_direction == Vector2.up && triggerCounter <= 0)
            {
                TurnDownForWhat();
            }
            else if (_direction == Vector2.down && triggerCounter <= 0)
            {
                TurnUp();
            }
            triggerCounter = triggerTimer;
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
            Debug.Log("Turtle hit Bounds and had to respawn");
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
