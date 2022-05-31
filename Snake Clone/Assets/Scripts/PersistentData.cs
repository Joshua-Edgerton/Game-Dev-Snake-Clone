using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    [Header("Variables Data")]
    public int totalScore;
    public int totalExp;
    public int currentLives;
    public int maxLives;
    public int levelLength;
    public int maxEnemies;
    public int startingSegmentSize;
    public float spawnIncreaseTimer;
    public int superFoodChance;
    [Header("Script Links")]
    public Abilities abilitiesScript;
    public Food foodScript;
    public Spawner spawnerScript;
    public StatsManager statsManagerScript;
    public Snake snakeScript;
    [Header("Scene Name List")]
    public List<string> _sceneNames = new List<string>();
    public int currentLevel = 1;
    [Header("Scene High Score List")]
    public List<int> _levelHighScores = new List<int>();
    [Header("Scene High Score List")]
    public List<int> _setHighScores = new List<int>();

    //Create a list for storing abilities that are gained
    //public List 

    private void Awake()
    {
        
    }

    void Start()
    {
        
        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);
        snakeScript = GameObject.Find("Snake").GetComponent<Snake>();
        abilitiesScript = GameObject.Find("Snake").GetComponent<Abilities>();
        foodScript = GameObject.Find("Food").GetComponent<Food>();
        spawnerScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        statsManagerScript = GameObject.Find("Level Manager").GetComponent<StatsManager>();

    }

    private void Update()
    {
        //DebugLogAllVariables();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //DebugLogHighScores();
            PermanentHighScoreSet();
            DebugLogSetHighScores();
        }
    }

    public void CallUpgradeScene()
    {
        SceneManager.LoadScene("Upgrade");
    }

    
    public void CallUpgradeSceneTimed()
    {
        if(statsManagerScript){statsManagerScript.hasWonLevel = false;}
        Invoke("CallUpgradeScene", 2f);
    }

    public void CallNextLevel()
    {
        SceneManager.LoadScene(_sceneNames[currentLevel]);
        currentLevel += 1;
        Debug.Log(_sceneNames[currentLevel]);
    }

    public void CallLoseScene()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        Application.Quit();
    }

    public void UpdatePersistentData()
    {
        if (snakeScript){snakeScript.UpdatePersistentData();}
        if (abilitiesScript){abilitiesScript.UpdatePersistentData();}
        if (spawnerScript){spawnerScript.UpdatePersistentData();}
        if (statsManagerScript){statsManagerScript.UpdatePersistentData();}
    }

    public void DebugLogAllVariables()
    {
        Debug.Log("Total Score: " + totalScore);
        Debug.Log("Total EXP: " + totalExp);
        Debug.Log("Current Lives: " + currentLives);
        Debug.Log("Max Lives: " + maxLives);
        Debug.Log("Level Length: " + levelLength);
        Debug.Log("Max Enemies: " + maxEnemies);
        Debug.Log("Spawn Increase Timer: " + spawnIncreaseTimer);
        Debug.Log("Super Food Chance: " + superFoodChance);
        Debug.Log("Starting Segments: " + startingSegmentSize);
    }

    public void DebugLogHighScores()
    {
        for (int i = 0; i < _levelHighScores.Count; i++)
        {
            Debug.Log("The level: " + _sceneNames[i] + " Has a High Score of " + _levelHighScores[i].ToString());
        }
    }
    public void DebugLogSetHighScores()
    {
        for (int i = 0; i < _setHighScores.Count; i++)
        {
            Debug.Log("High Score for Scene: " + _sceneNames[i] + " is: " + _setHighScores[i] + " points!");
        }
    }

    public void ReconnectScripts()
    {
        snakeScript = GameObject.Find("Snake").GetComponent<Snake>();
        abilitiesScript = GameObject.Find("Snake").GetComponent<Abilities>();
        foodScript = GameObject.Find("Food").GetComponent<Food>();
        spawnerScript = GameObject.Find("Enemy Manager").GetComponent<Spawner>();
        statsManagerScript = GameObject.Find("Level Manager").GetComponent<StatsManager>();     
    }

    public void PermanentHighScoreSet()
    {
        for (int i = 0; i < _levelHighScores.Count; i++)
        {
            if (_levelHighScores[i] > _setHighScores[i])
            {
                _setHighScores[i] = _levelHighScores[i];
                Debug.Log("Permanent High Score was replaced with new High Score");
            }
        }
    }

}
