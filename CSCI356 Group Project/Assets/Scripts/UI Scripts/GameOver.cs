using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public Text FinalScore;

    void Start()
    {
        FinalScore.text = EnterName.Score.ToString();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Save Scores Here...
        Leaderboard.SaveScore();

    }

    public void GoToMainMenu()
    {
        // Go back to main menu
        Debug.Log("Main Menu!");
        SceneManager.LoadScene("Main Menu");
    }
}
