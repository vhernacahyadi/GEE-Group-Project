using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //public float BouncingForce = 2f;
    //public Rigidbody PlayerBall;
   // [Range(100, 1000)]
    public float bounceHelight=500;
    //[SerializeField]private Animator playerAnimator;
    private bool onAir = false;
    //[SerializeField public Transform endMarker;
    private void Start()
    {
        //PlayerBall = GameObject.FindWithTag("TagOfBallObject").GetComponent<Rigidbody>();
    }

    //private void OnTriggerEnter(Collider other)
            private void OnCollisionEnter(Collision other)
    {
        GameObject bouncer = other.gameObject;
        Rigidbody rb = bouncer.GetComponent<Rigidbody>();
        GameObject rbBounce = GameObject.FindWithTag("Bounce");
        Debug.Log("going bounce");
        Vector3 desiredPosition = new Vector3(rbBounce.transform.position.x, 5, rbBounce.transform.position.z);
        if (other.gameObject.tag == "Player"&& onAir==false)
        {
            Debug.Log("on bounce");
            rb.AddForce(Vector3.up * bounceHelight);
            // rbBounce.transform.position = new Vector3(rbBounce.position.x, 5, rbBounce.position.z);

            rbBounce.transform.position = Vector3.MoveTowards(rbBounce.transform.position, desiredPosition, Time.deltaTime * 3f);
            onAir = true;
        }
    }
}