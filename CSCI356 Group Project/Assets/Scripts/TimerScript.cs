using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is up!");
                TimeLeft = 0;
                TimerOn = false;

                // end game function here..
                // Find the GameManager in the scene and run the EndGame() function
                FindObjectOfType<GameManager>().EndGame();
            }

        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
