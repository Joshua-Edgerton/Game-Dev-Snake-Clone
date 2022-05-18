using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : MonoBehaviour
{
    public BoxCollider2D enemyArea;
    private Vector2 _direction = Vector2.left;
    public float enemySpeed = 0.1f;
    public float randomDirectionTimer = 3;
    public float randomDirectionDefault = 2;
    public float randomDirectionCounter;
    public int randomDirectionChoice = 1;
    public Enemy enemyScript;
    public int turtleHealth = 30;
    public int amountHealedForFood = 10;
    public int amountHealedForSuperFood = 20;
    public Spawner spawnScript;
    public int expWorth = 2;
    public int scoreWorth = 200;
    public GameObject healthBarContainer;
    public HealthBar healthBarScript;
    public float triggerCounter;
    public float triggerTimer = 0.2f;
    void Start()
    {
        randomDirectionCounter = randomDirectionDefault;
        healthBarScript = healthBarContainer.GetComponent<HealthBar>();
        enemyScript.expToGive = expWorth;
        enemyScript.scoreToGive = scoreWorth;
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
                Debug.Log("Random Direction Invoked: " + randomDirectionTimer);
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
