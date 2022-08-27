using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FallPlatform : MonoBehaviour
{
	[SerializeField] private GameObject gameOverCamera;
	[SerializeField] private GameObject gameOverUI;

	// Start is called before the first frame update
	void Start()
    {
	}


    void OnCollisionStay(Collision cls)
	{
		if (cls.gameObject.name == "level")
		{
			Debug.Log ("Collision");
			/*	gameOverCamera.SetActive(true);
				gameOverUI.SetActive(true);
				Destroy(gameObject);*/
			if (SceneManager.GetActiveScene().name == "Level 2")
			{
				//Debug.Log("Level2");
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 + 1);
			}
			if (SceneManager.GetActiveScene().name == "Level 3")
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}

		}
	}
}
