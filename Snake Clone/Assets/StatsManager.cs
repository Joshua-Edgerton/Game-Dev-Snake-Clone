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
    public List<Transform> _lives = new List<Transform>();
    public GameObject lifeSprite;
    public int lifeSpriteAdjustment = 0;
    public int startingLives = 3;
    public int maxLives = 3;
    public int currentLives;

    public int totalExp;
    public int totalScore;

    void Start()
    {
        currentLives = startingLives;
        ResetLives();
    }

    void Update()
    {
        scoreCounter.text = totalScore.ToString();
        expCounter.text = totalExp.ToString();
        if (currentLives == 0)
        {
            GameOver();
        }
    }

    public void SegmentScore(int segmentsMade)
    {
        totalScore += (segmentsMade * 10);
    }
    public void KillExp(int killExp)
    {
        totalExp += killExp;
    }
    public void KillScore(int killScore)
    {
        totalScore += killScore;
    }

    public void LostLife()
    {
        currentLives -= 1;
        ResetLives();
    }

    public void ResetLives()
    {
        for (int i = 0; i < _lives.Count; i++)
        {
            _lives[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < currentLives + 1; i++)
        {
            _lives[i].gameObject.SetActive(true);
        }
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
