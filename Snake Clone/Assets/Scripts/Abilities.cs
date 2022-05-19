using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [Header("Script Links")]
    public Snake snakeScript;
    [Space(1)]
    [Header("Game Objects")]
    public GameObject venomBall;
    [Space(1)]
    [Header("Venom Spit Ability")]
    public bool isVenomSpit = true;
    public int venomSpitCost = 1;

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
        }
    }
    public void VenomSpit()
    {
        Instantiate(venomBall, snakeScript.aim.transform.position, snakeScript.aim.transform.rotation);
    }
}
