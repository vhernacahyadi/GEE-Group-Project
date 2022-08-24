using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject player;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = EnterName.Score.ToString();
    }
}
