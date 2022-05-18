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
    public int levelLength = 120;
    public float levelLengthCounter;
    public Text levelLengthUI;
    public int numberOfMinutes;

    public int totalExp;
    public int totalScore;
    public bool pauseTimer = false;
    public bool gamePaused = false;

    void Start()
    {
        currentLives = startingLives;
        ResetLives();
        levelLengthCounter = levelLength;
    }

    void Update()
    {
        scoreCounter.text = totalScore.ToString();
        expCounter.text = totalExp.ToString();
        if (!pauseTimer)
        {
            levelLengthCounter -= Time.deltaTime;
        }
        //levelLengthUI.text = (Mathf.Round(levelLengthCounter) / 60).ToString("00") + ":" + (Mathf.Round(levelLengthCounter) % 60).ToString("00");
        int minutes = Mathf.FloorToInt(levelLengthCounter / 60F);
        int seconds = Mathf.FloorToInt(levelLengthCounter - minutes * 60);
        levelLengthUI.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        Debug.Log(gamePaused);

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

        for (int i = 0; i < currentLives; i++)
        {
            _lives[i].gameObject.SetActive(true);
        }
    }
    public void PauseGame()
    {
        if (gamePaused == false)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
