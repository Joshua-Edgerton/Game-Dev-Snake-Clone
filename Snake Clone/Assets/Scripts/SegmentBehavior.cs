using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBehavior : MonoBehaviour
{
    public Snake snakeScript;
    void Start()
    {
        snakeScript = GameObject.Find("Snake").GetComponent<Snake>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Obstacle")
        {
            Debug.Log("Hit enemy, obstacle");
            snakeScript.ResetState();
        }
    }

}
