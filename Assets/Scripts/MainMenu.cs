using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //no functionality to populate teams browser or to enter private game password yet - will implement after multiplayer functionality 
    //the back button in the teams screen doesn't work correctly because idk how the game will know if you got there from the host or join screen

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }

    public void GoBack()
    {
        if (SceneManager.GetActiveScene().name == "Teams")
        {
            //would need to know which screen was last - host or join, https://stackoverflow.com/questions/39377785/how-do-i-load-previous-scene-on-unity3d
            //https://forum.unity.com/threads/how-can-i-open-previous-scene.652507/
            //https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html
            //for now we go back to join
            SceneManager.LoadScene("GameBrowser");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); 
        }
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GameSelect()
    {
        SceneManager.LoadScene("GameSelect");
    }

    public void HostGame()
    {
        SceneManager.LoadScene("HostGame");
    }

    public void JoinGame()
    {
        SceneManager.LoadScene("GameBrowser");
    }

    public void TeamScreen()
    {
        SceneManager.LoadScene("Teams");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("quit game"); //it won't quit the game in unity preview, only when built, so the text shows it's working
        Application.Quit();
    }
}
