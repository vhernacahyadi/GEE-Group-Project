using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int currentScore = EnterName.Score;
    public static int bulletAmount = 20;

    public GameObject CompleteLevelUI;

    public void CompleteLevel()
    {
        Debug.Log("Level Complete!");
        CompleteLevelUI.SetActive(true);
    }

    // Open up CompleteLevel Room after EndGame
    public void EndGame()
    {
        Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            GameObject cannon = GameObject.Find("Cannon"); // Finds a game object by name
            cannon.transform.position = new Vector3(cannon.transform.position.x, 3.5f, cannon.transform.position.z);
        }

        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            // activate button
            GameObject sw = GameObject.Find("Switch"); // Finds a game object by name
            sw.transform.position = new Vector3(sw.transform.position.x, 2.5f, sw.transform.position.z);
        }

        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            // Go to the next scene [Game Over Scene]
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Destroy all slimes on scene
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < slimes.Length; i++)
        {
            Destroy(slimes[i]);
        }


        Debug.Log("Game Over!");
    }

    public void AddScore(float amount)
    {
        Debug.Log("SCORE ADDED!");
        EnterName.Score += (int)amount;
    }
}
