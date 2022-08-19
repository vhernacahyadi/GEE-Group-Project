using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollidePlatform : MonoBehaviour
{
	private GameObject player;
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
			Destroy(gameObject);
		}
	}
}
