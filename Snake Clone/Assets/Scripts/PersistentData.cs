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
    public int highScoreLevel01;
    public int highScoreLevel02;
    public int highScoreLevel03;
    public int highScoreLevel04;
    public int highScoreLevel05;
    [Header("Script Links")]
    public Abilities abilitiesScript;
    public Food foodScript;
    public Spawner spawnerScript;
    public StatsManager statsManagerScript;
    public Snake snakeScript;

    //Create a list for storing abilities that are gained
    //public List 

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

    }

    public void CallNextScene()
    {
        //statsManagerScript.PauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //statsManagerScript.hasWonLevel = false;
    }

    
    public void CallNextSceneTimed()
    {
        statsManagerScript.hasWonLevel = false;
        //statsManagerScript.PauseGame();
        Invoke("CallNextScene", 2f);
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
        snakeScript.UpdatePersistentData();
        abilitiesScript.UpdatePersistentData();
        foodScript.UpdatePersistentData();
        spawnerScript.UpdatePersistentData();
        statsManagerScript.UpdatePersistentData();
    }
}
