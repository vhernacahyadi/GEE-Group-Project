using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExplosion2 : MonoBehaviour {

	private float radius = 5.0f;
	private float power = 50.0f;
	private Rigidbody target;

	public GameObject explosion;    //public Transform explosion;  works too

	void Start () {	
		target = GetComponent<Rigidbody>();	
	}

	void HitByExplosiveShell( Vector3 direction ) // process a message HitByExplosiveShell
	{
		//target.AddExplosionForce (power/2.0f, transform.position, radius, 0f, ForceMode.Impulse);  // try ForceMode.Force too
		//target.AddForce ( direction*power, ForceMode.Impulse );  // try ForceMode.Force too

		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
