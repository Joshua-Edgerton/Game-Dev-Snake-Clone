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

    public GameObject scoresMenu;
    public GameObject replayMenu;
    public List<Text> _scores;

    private void Start()
    {
        persistentDataScript = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
        totalScore.text = persistentDataScript.totalScore.ToString();

        for (int i = 0; i < persistentDataScript._setHighScores.Count; i++)
        {
            _scores[i].text = persistentDataScript._setHighScores[i].ToString();
        }
    }
    private void Update()
    {
        totalScore.text = persistentDataScript.totalScore.ToString();
    }

    public void ReplayGame()
    {
        persistentDataScript.ReturnToMainMenu();
    }

    public void OpenScoresMenu()
    {
        replayMenu.gameObject.SetActive(false);
        scoresMenu.gameObject.SetActive(true);
    }

    public void OpenReplayMenu()
    {
        replayMenu.gameObject.SetActive(true);
        scoresMenu.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        Application.Quit();
    }
}
