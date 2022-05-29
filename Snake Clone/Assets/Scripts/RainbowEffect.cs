using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowEffect : MonoBehaviour
{
    public float rainbowSpeed;
    private float hue;
    private float saturation;
    private float brightness;
    private MeshRenderer meshRenderer;
    private Text rainbowText;

    void Start()
    {
        rainbowText = this.GetComponent<Text>();
    }

    void Update()
    {
        Color.RGBToHSV(rainbowText.color, out hue, out saturation, out brightness);
        hue += rainbowSpeed / 10000;
        if (hue >= 1)
        {
            hue = 0;
        }
        saturation = 1;
        brightness = 1;
        rainbowText.color = Color.HSVToRGB(hue, saturation, brightness);
    }
}
