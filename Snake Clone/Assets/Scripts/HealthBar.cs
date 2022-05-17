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
    public bool isTurtle = false;

    private void Update()
    {
        if (isPorcupine)
        {
            heightAboveEnemy = 3.5f;
        }
        else if (isTurtle)
        {
            heightAboveEnemy = 3;
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
