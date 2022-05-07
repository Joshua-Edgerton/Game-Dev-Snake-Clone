using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public GameObject venomBall;
    public void VenomSpit(Transform aim)
    {
        Instantiate(venomBall, aim);
    }
}
