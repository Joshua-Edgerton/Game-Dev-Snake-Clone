using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBehavior : MonoBehaviour
{
    [Header("Script Links")]
    public Snake snakeScript;
    [Space(1)]
    [Header("Snake Growth 'animation'")]
    public float snakeGrowthStart = 0;
    void Start()
    {
        StartCoroutine(SnakeSize());
        snakeScript = GameObject.Find("Snake").GetComponent<Snake>();
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    IEnumerator SnakeSize()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (snakeGrowthStart < 0.9f)
            {
                snakeGrowthStart += 0.1f;
                this.gameObject.transform.localScale = new Vector3(snakeGrowthStart, snakeGrowthStart, snakeGrowthStart);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Obstacle")
        {
            Debug.Log("Hit enemy, obstacle");
            snakeScript.DieThenChooseSpawn();
        }
    }

}
