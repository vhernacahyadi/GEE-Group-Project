using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // Get audio volume from PlayerPrefs
        AudioListener.volume = PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : 1.0f;
    }

    public void QuitGame()
    {
        // Close the Game / Application
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
