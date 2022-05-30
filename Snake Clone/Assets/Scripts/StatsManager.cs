using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [Header("UI Text")]
    public Text segmentCounter;
    public Text scoreCounter;
    public Text expCounter;
    public Text spawnCounter;
    public Text maxEnemiesUI;
    public GameObject spawnCounterUI;
    public GameObject victory;
    [Space(1)]
    [Header("Script Links")]
    public Spawner SpawnerScript;
    public PersistentData persistentDataScript;
    [Space(1)]
    [Header("Lists")]
    public List<Transform> _lives = new List<Transform>();
    [Space(1)]
    [Header("Life Sprites/Controls")]
    public GameObject lifeSprite;
    public int lifeSpriteAdjustment = 0;
    public int startingLives = 3;
    public int maxLives = 3;
    public int currentLives;
    [Space(1)]
    [Header("Level Adjustments")]
    public int levelLength = 30;
    public float levelLengthCounter;
    public Text levelLengthUI;
    public int numberOfMinutes;
    public bool pauseTimer = false;
    public bool gamePaused = false;
    public float spawnIncreaseTimeDefault;
    public float spawnIncreaseTimer;
    public bool hasWonLevel = false;
    [Space(1)]
    [Header("Total Stats")]
    public int totalExp;
    public int totalScore;

    void Start()
    {
        currentLives = startingLives;
        ResetLives();
        levelLengthCounter = levelLength;
        spawnIncreaseTimer = spawnIncreaseTimeDefault;
        SpawnerScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        persistentDataScript = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
    }

    void Update()
    {
        if (levelLengthCounter <= 0 && currentLives > 0 && hasWonLevel == false)
        {
            Victory();
            hasWonLevel = true;
        }

        spawnIncreaseTimer -= Time.deltaTime;

        if (spawnIncreaseTimer <= 0)
        {
            SpawnerScript.maxEnemies += 1;
            //Debug.Log("Enemy max is: " + SpawnerScript.maxEnemies);
            spawnIncreaseTimer = spawnIncreaseTimeDefault;
        }

        scoreCounter.text = totalScore.ToString();
        expCounter.text = totalExp.ToString();
        maxEnemiesUI.text = SpawnerScript.maxEnemies.ToString();

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

    public void Victory()
    {
        victory.SetActive(true);
        persistentDataScript.UpdatePersistentData();
        persistentDataScript.CallNextSceneTimed();
        //PauseGame();
    }

    public void GameOver()
    {

    }

    public void UpdatePersistentData()
    {
        persistentDataScript.currentLives = currentLives;
        persistentDataScript.maxLives = maxLives;
        persistentDataScript.totalExp += totalExp;
        persistentDataScript.totalScore += totalScore;
        persistentDataScript.levelLength = levelLength;
    }
}
