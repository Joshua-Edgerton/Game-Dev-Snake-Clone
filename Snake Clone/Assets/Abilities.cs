using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Snake snakeScript;
    public GameObject venomBall;
    public bool isVenomSpit = true;
    public int venomSpitCost = 1;

    private void Awake()
    {

    }
    private void Start()
    {
        snakeScript = GameObject.Find("Snake").GetComponent<Snake>();
    }
    public void PlayAbility()
    {
        if (isVenomSpit)
        {
            VenomSpit();
            snakeScript.DestroySegments(venomSpitCost);
            Debug.Log("Venom spit attempted, destroy segments");
        }
    }
    public void VenomSpit()
    {
        Instantiate(venomBall, snakeScript.aim.transform.position, snakeScript.aim.transform.rotation);
        Debug.Log("Instantiate venom");
    }
}
