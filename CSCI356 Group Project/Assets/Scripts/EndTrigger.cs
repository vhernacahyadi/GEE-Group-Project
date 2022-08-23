using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public GameManager gameManager;

    void OnTriggerEnter(Collider collider)
    {
        // Avoid any other object colliding to complete the game
        if (collider.gameObject.tag == "Player")
        {
            gameManager.CompleteLevel();
        }   
    }
}
