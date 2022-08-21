using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollidePlatform : MonoBehaviour
{
	private GameObject player;
	[SerializeField] GameObject gameOverCamera;
	[SerializeField] GameObject gameOverUI;
	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindWithTag("Player");

	}


    void OnCollisionStay(Collision cls)
	{
		if (cls.gameObject.name == "level")
		{
			Debug.Log ("Collision");
			gameOverCamera.SetActive(true);
			gameOverUI.SetActive(true);
			Destroy(gameObject);
		}
	}
}
