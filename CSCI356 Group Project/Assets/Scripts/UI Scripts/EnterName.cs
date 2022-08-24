using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterName : MonoBehaviour
{
    public static string Name;
    public static int Score = 0;
    public InputField inputName;
    public Text WarningText;

    public void InputName()
    {
        if (string.IsNullOrEmpty(inputName.text))
            WarningText.gameObject.SetActive(true);     
        else
        {
            WarningText.gameObject.SetActive(false);
            Name = inputName.text;
            Debug.Log("Name: " + Name);
            PlayGame();
        } 
    }

    public void PlayGame()
    {
        // Load First Stage of the game
        Debug.Log("Game Start!");
        SceneManager.LoadScene("Level 1");
    }
}
