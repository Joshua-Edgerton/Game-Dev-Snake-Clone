using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [Header("Script Links")]
    public Snake snakeScript;
    public PersistentData persistentDataScript;
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
        persistentDataScript = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
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

    public void UpdatePersistentData()
    {
        //Add all abilities gained to persistent data list
    }
}
