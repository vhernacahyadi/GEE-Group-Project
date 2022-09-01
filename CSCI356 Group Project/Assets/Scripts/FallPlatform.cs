using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FallPlatform : MonoBehaviour
{
	[SerializeField]
	private Transform startPoint;

	// Start is called before the first frame update
	void Start()
    {

	}


    void OnCollisionEnter(Collision cls)
	{
		if (cls.gameObject.tag == "Player")
		{
			startPoint.position += new Vector3(0, 1.0f, 0);
			cls.gameObject.transform.position = startPoint.position;
		}
	}
}
