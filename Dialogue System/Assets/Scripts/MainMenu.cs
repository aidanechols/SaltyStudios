using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Exit()
    {
        Debug.Log("Exit pressed");
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
}
