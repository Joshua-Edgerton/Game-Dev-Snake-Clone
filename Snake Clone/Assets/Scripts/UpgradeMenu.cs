using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{

    public PersistentData persistentDataScript;

    private void Start()
    {
        persistentDataScript = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
    }

    public void ContinueGame()
    {
        persistentDataScript.CallNextLevel();
        //SceneManager.LoadScene("Level01");
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        Application.Quit();
    }
}
