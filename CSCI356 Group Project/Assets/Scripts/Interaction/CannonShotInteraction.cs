using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShotInteraction : MonoBehaviour {

	private AudioSource gunAudio;
	private RaycastHit hitInfo;


	// Use this for initialization
	void Start () 
	{
		gunAudio = GetComponent<AudioSource>();
	}

	public void Shoot()
    {
		gunAudio.Play();

		Vector3 directiononOfFire = transform.up; // because the gun barrel (cylinder) has been rotated from vertical to horizontal

		if (Physics.Raycast(transform.position, directiononOfFire, out hitInfo, 100))
		{
			// Send a message to the object corresponding to hitInfo
			hitInfo.transform.SendMessage("HitByExplosiveShell", directiononOfFire, SendMessageOptions.DontRequireReceiver);
		}

		// Show a line of fire in  the scene view. Disable after debugging
		Debug.DrawLine(transform.position, hitInfo.point, Color.yellow);
	}

}
