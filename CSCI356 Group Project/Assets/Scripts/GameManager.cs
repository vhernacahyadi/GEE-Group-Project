using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static float currentScore = EnterName.Score;
    public static int bulletAmount = 20;

    public GameObject CompleteLevelUI;

    public void CompleteLevel()
    {
        Debug.Log("Level Complete!");
        CompleteLevelUI.SetActive(true);
    }

    public void EndGame()
    {
        Debug.Log("Game Over!");
    }

    public void AddScore(float amount)
    {
        Debug.Log("SCORE ADDED!");
        currentScore += amount;
    }
}
