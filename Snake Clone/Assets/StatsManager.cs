using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public Text segmentCounter;
    public Text scoreCounter;
    public Text expCounter;
    public Text spawnCounter;
    public GameObject spawnCounterUI;

    public int totalExp;

    void Start()
    {

    }

    void Update()
    {
        scoreCounter.text = totalExp.ToString();
    }

    public void SegmentExp(int segmentsMade)
    {
        totalExp += (segmentsMade * 10);
    }
}
