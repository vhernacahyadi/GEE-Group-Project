using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load First Stage of the game
        Debug.Log("Game Start!");
        // SceneManager.LoadScene(...);
    }

    public void QuitGame()
    {
        // Close the Game / Application
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
