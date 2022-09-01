using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FallPlatform : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
    {
	}


    void OnCollisionEnter(Collision cls)
	{
		if (cls.gameObject.tag == "Player")
		{
			if (SceneManager.GetActiveScene().name == "Level 2")
			{
				SceneManager.LoadScene("Level 2");
			}
		}
	}
}
