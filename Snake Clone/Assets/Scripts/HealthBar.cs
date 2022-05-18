using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject enemy;
    public float healthBarOffset;
    public float heightAboveEnemy = 3;
    public bool isPorcupine = false;
    public bool isPorcupineSideways = false;
    public bool isTurtle = false;
    public bool isTurtleSideways = false;
    public bool isBoar = false;
    public bool isBoarSideways = false;

    private void Update()
    {
        if (isPorcupine)
        {
            if (isPorcupineSideways)
            {
                heightAboveEnemy = 3;
            }
            else
            {
                heightAboveEnemy = 3.5f;
            }
        }
        else if (isTurtle)
        {
            if (isTurtleSideways)
            {
                heightAboveEnemy = 2.5f;
            }
            else
            {
                heightAboveEnemy = 3;
            }
        }
        else if (isBoar)
        {
            if (isBoarSideways)
            {
                heightAboveEnemy = 3;
            }
            else
            {
                heightAboveEnemy = 6;
            }
        }
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + healthBarOffset + heightAboveEnemy, 0);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
