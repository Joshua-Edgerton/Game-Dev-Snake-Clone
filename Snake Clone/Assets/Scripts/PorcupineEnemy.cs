using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineEnemy : MonoBehaviour
{
    [Header("Enemy Bounds")]
    public BoxCollider2D enemyArea;
    [Space(1)]
    [Header("Script Links")]
    public Enemy enemyScript;
    public Spawner spawnScript;
    public HealthBar healthBarScript;
    [Space(1)]
    [Header("Movement")]
    private Vector2 _direction = Vector2.left;
    public float enemySpeedDefault = 0.2f;
    public float enemySpeed = 0.2f;
    public float enemySpeedPaused = 3;
    public float randomDirectionTimer = 3;
    public int randomDirectionChoice = 1;
    public float triggerCounter;
    public float triggerTimer = 1;
    [Space(1)]
    [Header("Health/Stats/EXP")]
    [Range(1, 100)]
    public int porcupineHealth = 80;
    [Range(1, 100)]
    public int amountHealedForFood = 20;
    [Range(1, 100)]
    public int amountHealedForSuperFood = 40;
    [Range(1, 10)]
    public int expWorth = 4;
    [Range(100, 10000)]
    public int scoreWorth = 500;
    [Space(1)]
    [Header("Game Objects")]
    public List<Transform> _spineLocations = new List<Transform>();
    public GameObject spikeProjectile;
    public GameObject healthBarContainer;
    void Start()
    {
        healthBarScript = healthBarContainer.GetComponent<HealthBar>();
        enemyScript.expToGive = expWorth;
        enemyScript.scoreToGive = scoreWorth;
        spawnScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        StartCoroutine(PorcupineUpdate());
        StartCoroutine(PauseThenShootCounter());
        enemyScript = this.GetComponent<Enemy>();
        enemyScript.enemyHealthMax = porcupineHealth;
        int randomDirectionStart = Random.Range(1, 5);

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
        if (triggerCounter > 0)
        {
            triggerCounter -= Time.deltaTime;
        }
        if (_direction == Vector2.left || _direction == Vector2.right)
        {
            healthBarScript.isPorcupineSideways = true;
        }
        else
        {
            healthBarScript.isPorcupineSideways = false;
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

    IEnumerator PorcupineUpdate()
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

    IEnumerator PauseThenShootCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            PauseThenShoot();
        }
    }
    //The games default update speed
    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle" || other.tag == "Enemy" || other.tag == "Enemy Obstacle")
        {
            Debug.Log("Hit trigger");
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
            randomDirectionTimer = Random.Range(4f, 11f);
            randomDirectionChoice = Random.Range(1, 3);
            Invoke("RandomDirection", randomDirectionTimer);
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
            Debug.Log("Enemy went past bounds");
            Destroy(gameObject);
            if (spawnScript.currentEnemyCount! >= spawnScript.maxEnemies)
            {
                spawnScript.FirstSpawn();
            }
        }
    }

    private void TurnRight()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _direction = Vector2.right;
    }
    private void TurnLeft()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, -180f);
        _direction = Vector2.left;
    }
    private void TurnUp()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        _direction = Vector2.up;
    }
    private void TurnDownForWhat()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        _direction = Vector2.down;
    }
    private void PauseThenShoot()
    {
        enemySpeed = enemySpeedPaused;
        Invoke("Shoot", 2);
        Invoke("Shoot", 2.5f);
        Invoke("Shoot", 3);
    }
    private void Shoot()
    {
        enemySpeed = enemySpeedDefault;
        for (int i = 0; i < _spineLocations.Count; i++)
        {
            Instantiate(spikeProjectile, _spineLocations[i].transform.position, _spineLocations[i].transform.rotation);
        }
    }
}
