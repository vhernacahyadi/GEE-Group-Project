using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject player;
    public GameObject playerCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable player shooting & moving when resumed
        player.GetComponent<gunShooter>().enabled = true;
        playerCamera.GetComponent<RotationController>().enabled = true;

        // Unfreeze the game
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable player shooting & moving when paused
        player.GetComponent<gunShooter>().enabled = false;
        playerCamera.GetComponent<RotationController>().enabled = false;

        // Freeze the game
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitMenu()
    {
        Debug.Log("Quitting Game...");

        // Unfreeze the game
        Time.timeScale = 1f;

        // Go to Main Menu [Game Over Scene]
        SceneManager.LoadScene("Main Menu");
    }
}
