using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarEnemy : MonoBehaviour
{
    public BoxCollider2D enemyArea;
    public Vector2 _direction = Vector2.right;
    public float enemySpeed = 0.2f;
    public float enemySpeedDefault = 0.2f;
    public float enemySpeedCharge = 0.05f;
    public float enemySpeedPause = 1f;
    public Enemy enemyScript;
    public int boarHealth = 80;
    public int amountHealedForFood = 20;
    public int amountHealedForSuperFood = 40;
    public Spawner spawnScript;
    public int expWorth = 4;
    public int scoreWorth = 500;
    public float turnAroundTime = 0.5f;
    public float turnAroundCounter;
    public float raycastHitTime = 0.2f;
    public float raycastHitCounter;
    public int timesCrossedSnakeUp;
    public int timesCrossedSnakeDown;
    private Animator anim;
    public bool hitWallAfterCharge = false;
    public GameObject healthBarContainer;
    public HealthBar healthBarScript;
    // Start is called before the first frame update
    void Start()
    {
        healthBarScript = healthBarContainer.GetComponent<HealthBar>();
        enemyScript.expToGive = expWorth;
        enemyScript.scoreToGive = scoreWorth;
        spawnScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        enemyScript = this.GetComponent<Enemy>();
        enemyScript.enemyHealthMax = boarHealth;
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(BoarUpdate());
        anim.SetBool("isCharging", false);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 30f, Color.red);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 30f, Color.red);
        LayerMask snakeMask = LayerMask.GetMask("SnakeHead");
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 30f, snakeMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 30f, snakeMask);

        if (_direction == Vector2.left || _direction == Vector2.right)
        {
            healthBarScript.isBoarSideways = true;
        }
        else
        {
            healthBarScript.isBoarSideways = false;
        }

        if (raycastHitCounter > 0)
        {
            raycastHitCounter -= Time.deltaTime;
        }
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
        if (turnAroundCounter > 0)
        {
            turnAroundCounter -= Time.deltaTime;
        }
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle" || other.tag == "Enemy" || other.tag == "Enemy Obstacle")
        {
            if (hitWallAfterCharge)
            {
                enemySpeed = enemySpeedPause;
                Invoke("TurnAround", 1);
            }
            else
            {
                Debug.Log("boar collided with obstacle or enemy");
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
    private void TurnRight()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _direction = Vector2.right;
        Debug.Log("Turned Right");
    }
    private void TurnLeft()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        _direction = Vector2.left;
        Debug.Log("Turned Left");
    }
    private void TurnUp()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        _direction = Vector2.up;
        Debug.Log("Turned Up");
    }
    private void TurnDownForWhat()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        _direction = Vector2.down;
        Debug.Log("Turned Down");
    }
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
        //StopCoroutine(BoarUpdate());
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
    public void ChargeAttack()
    {
        anim.SetBool("isCharging", false);
        enemySpeed = enemySpeedCharge;
        hitWallAfterCharge = true;

    }
    public void flashRed()
    {

    }
}
