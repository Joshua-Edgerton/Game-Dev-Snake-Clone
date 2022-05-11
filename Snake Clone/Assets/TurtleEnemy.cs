using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : MonoBehaviour
{
    public BoxCollider2D enemyArea;
    private Vector2 _direction = Vector2.left;
    public float enemySpeed = 0.5f;
    public float randomDirectionTimer = 3;
    public int randomDirectionChoice = 1;
    void Start()
    {
        RandomizePosition();
        //Coroutine for custom update speed
        StartCoroutine(TurtleUpdate());

    }
    void Update()
    {

    }

    IEnumerator RandomDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(randomDirectionTimer);
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
                StopCoroutine(RandomDirection());
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
                StopCoroutine(RandomDirection());
                randomDirectionChoice = 0;
            }
            StopCoroutine(RandomDirection());

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

    private void RandomizePosition()
    {
        Bounds bounds = this.enemyArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            randomDirectionTimer = Random.Range(4f, 11f);
            randomDirectionChoice = Random.Range(1, 3);
            StartCoroutine(RandomDirection());

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
    }

    private void TurnRight()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        _direction = Vector2.right;
        Debug.Log("Turned Right");
    }
    private void TurnLeft()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _direction = Vector2.left;
        Debug.Log("Turned Left");
    }
    private void TurnUp()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        _direction = Vector2.up;
        Debug.Log("Turned Up");
    }
    private void TurnDownForWhat()
    {
        this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        _direction = Vector2.down;
        Debug.Log("Turned Down");
    }
}
