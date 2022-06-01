using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayMenu : MonoBehaviour
{
    public PersistentData persistentDataScript;
    public GameObject youDied;
    public GameObject needsWorkText;
    public Text totalScore;

    private void Start()
    {
        persistentDataScript = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
        totalScore.text = persistentDataScript.totalScore.ToString();
    }
    private void Update()
    {
        totalScore.text = persistentDataScript.totalScore.ToString();
    }

    public void ReplayGame()
    {
        persistentDataScript.ReturnToMainMenu();
    }

    public void CheckScores()
    {
        Debug.Log("Not set up yet - in replay menu script");
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        Application.Quit();
    }
}
