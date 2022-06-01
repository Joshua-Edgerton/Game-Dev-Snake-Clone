using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarEnemy : MonoBehaviour
{
    [Header("Script Links")]
    public Enemy enemyScript;
    public Spawner spawnScript;
    public HealthBar healthBarScript;
    [Space(1)]
    [Header("Enemy Bounds")]
    public BoxCollider2D enemyArea;
    [Space(1)]
    [Header("Movement")]
    public Vector2 _direction = Vector2.right;
    public float turnAroundTime = 0.5f;
    public float turnAroundCounter;
    public float enemySpeed = 0.2f;
    public float enemySpeedDefault = 0.2f;
    public float enemySpeedCharge = 0.05f;
    public float enemySpeedPause = 1f;
    public bool hitWallAfterCharge = false;
    [Space(1)]
    [Header("Raycast for Snake")]
    public float raycastHitTime = 0.2f;
    public float raycastHitCounter;
    public int timesCrossedSnakeUp;
    public int timesCrossedSnakeDown;
    [Space(1)]
    [Header("Health/Stats/Exp")]
    [Range(1, 100)]
    public int boarHealth = 80;
    [Range(1, 100)]
    public int amountHealedForFood = 20;
    [Range(1, 100)]
    public int amountHealedForSuperFood = 40;
    [Range(1, 10)]
    public int expWorth = 4;
    [Range(100, 10000)]
    public int scoreWorth = 500;
    [Space(1)]
    [Header("Animation")]
    private Animator anim;
    [Space(1)]
    [Header("Game Objects")]
    public GameObject healthBarContainer;

    void Start()
    {
        //Script Linking
        healthBarScript = healthBarContainer.GetComponent<HealthBar>();
        spawnScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        //Component Linking
        anim = gameObject.GetComponent<Animator>();
        enemyScript = this.GetComponent<Enemy>();
        //Changing variabls on other scripts
        enemyScript.expToGive = expWorth;
        enemyScript.scoreToGive = scoreWorth;
        enemyScript.enemyHealthMax = boarHealth;
        //Coroutines
        StartCoroutine(BoarUpdate());
        //Animation
        anim.SetBool("isCharging", false);
    }

    void Update()
    {
        LayerMask boundsMask = LayerMask.GetMask("Bounds");
        //Raycast that finds the object "Bounds" whenever it is in front of the enemy, and returns "true" if it is within range
        RaycastHit2D boundsInFront = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 7f, boundsMask);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 7f, Color.red);

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
        //Layer Mask that is used in unison with Raycast2D to find a specific layer
        LayerMask snakeMask = LayerMask.GetMask("SnakeHead");
        //Raycast that finds the object "SnakeHead" whenever it is to the left or right of the boar, and returns "true" if it is
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 30f, snakeMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 30f, snakeMask);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 10f, Color.red);

        //If statement that lets the healthbar script know if this character is sideways or not, to adjust for health bar height
        if (_direction == Vector2.left || _direction == Vector2.right)
        {
            healthBarScript.isBoarSideways = true;
        }
        else
        {
            healthBarScript.isBoarSideways = false;
        }
        //If the raycastHitCounter is greater than 0, the counterr starts counting down
        //The counter limits the amount of times the snake is scanned in a certain amount of time
        if (raycastHitCounter > 0)
        {
            raycastHitCounter -= Time.deltaTime;
        }
        //If the countdown has finished, the raycast can now scan the snake again, and adds up the amount of times its crossed the snake
        if (raycastHitCounter <= 0)
        {
            if (hitUp)
            {
                timesCrossedSnakeUp += 1;
                raycastHitCounter = raycastHitTime;
            }
            else if (hitDown)
            {
                timesCrossedSnakeDown += 1;
                raycastHitCounter = raycastHitTime;
            }
        }
        //If the boar has crossed the snake 3 times, from either side, it will charge in the direction of the snake
        if (timesCrossedSnakeUp == 3)
        {
            ChargeLeft();
            timesCrossedSnakeUp = 0;
            timesCrossedSnakeDown = 0;
        }
        if (timesCrossedSnakeDown == 3)
        {
            ChargeRight();
            timesCrossedSnakeDown = 0;
            timesCrossedSnakeUp = 0;
        }
        //This turnAroundCounter limits how often the TurnAround() function can be called, so that it isnt triggered back to back
        if (turnAroundCounter > 0)
        {
            turnAroundCounter -= Time.deltaTime;
        }
    }
    //This IEnumerator works as a fixed update - the "enemySpeed" variable essentially controls how often the rest of the code runs
    //This is used specifically for controlling the movement of the enemy, while making sure it only moves in whole integer values with Mathf.Round()
    IEnumerator BoarUpdate()
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
    //Whenever this objects Box Collider hits another Box Collider (with the trigger enabled) get that objects data stored as "other"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle" || other.tag == "Enemy" || other.tag == "Enemy Obstacle")
        {
            //This "stuns" the boar after hitting a wall when its charging - The invoke function makes it wait to turn around
            if (hitWallAfterCharge)
            {
                enemySpeed = enemySpeedPause;
                Invoke("TurnAround", 1);
            }
            else
            {
                TurnAround();
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
        //This kills the enemy and spawns another one if it happens to cross the outer limits with the tag "Bounds"
        if (other.tag == "Bounds")
        {
            Debug.Log("Boar hit Bounds and had to respawn");
            Destroy(gameObject);
            if (spawnScript.currentEnemyCount! >= spawnScript.maxEnemies)
            {
                spawnScript.FirstSpawn();
            }
        }
    }
    //Turns the enemy around after hitting a wall, depending on its direction
    private void TurnAround()
    {
        enemySpeed = enemySpeedDefault;
        hitWallAfterCharge = false;
        if (_direction == Vector2.right && turnAroundCounter <= 0)
        {
            TurnLeft();
            turnAroundCounter = turnAroundTime;
        }
        if (_direction == Vector2.left && turnAroundCounter <= 0)
        {
            TurnRight();
            turnAroundCounter = turnAroundTime;
        }
        if (_direction == Vector2.up && turnAroundCounter <= 0)
        {
            TurnDownForWhat();
            turnAroundCounter = turnAroundTime;
        }
        if (_direction == Vector2.down && turnAroundCounter <= 0)
        {
            TurnUp();
            turnAroundCounter = turnAroundTime;
        }
    }
    //Specific functions for turning each direction
    private void TurnRight()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _direction = Vector2.right;
    }
    private void TurnLeft()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
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
    //Pauses and then invokes the charge attack towards the snake after passing it a certain amount of times
    public void ChargeLeft()
    {
        anim.SetBool("isCharging", true);
        enemySpeed = enemySpeedPause;
        if (_direction == Vector2.up)
        {
            TurnLeft();
        }
        else if (_direction == Vector2.left)
        {
            TurnDownForWhat();
        }
        else if (_direction == Vector2.down)
        {
            TurnRight();
        }
        else if (_direction == Vector2.right)
        {
            TurnUp();
        }
        Invoke("ChargeAttack", 1f);
    }
    public void ChargeRight()
    {
        enemySpeed = enemySpeedPause;
        anim.SetBool("isCharging", true);
        if (_direction == Vector2.up)
        {
            TurnRight();
        }
        else if (_direction == Vector2.left)
        {
            TurnUp();
        }
        else if (_direction == Vector2.down)
        {
            TurnLeft();
        }
        else if (_direction == Vector2.right)
        {
            TurnDownForWhat();
        }
        Invoke("ChargeAttack", 1f);
    }
    //Charges the snake, and turns "hitWallAfterCharge" to true,so that when it hits the next wall it will be "stunned"
    public void ChargeAttack()
    {
        anim.SetBool("isCharging", false);
        enemySpeed = enemySpeedCharge;
        hitWallAfterCharge = true;

    }
    //A function to visually indicate the boar is about to charge
    public void flashRed()
    {

    }
}
