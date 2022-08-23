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
    GameObject rbBounce;
    Vector3 originalPosition;
    Vector3 desiredPosition;
    //[SerializeField public Transform endMarker;
    private void Start()
    {
        rbBounce = GameObject.FindWithTag("Bounce");
        Debug.Log("going bounce");
        originalPosition = rbBounce.transform.position;
        desiredPosition = new Vector3(rbBounce.transform.position.x, rbBounce.transform.position.y + 10, rbBounce.transform.position.z);
    }

    //private void OnTriggerEnter(Collider other)
    private void OnCollisionEnter(Collision other)
    {
        GameObject bouncer = other.gameObject;
        Rigidbody rb = bouncer.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("on bounce");
            rb.AddForce(Vector3.up * bounceHelight);
            // rbBounce.transform.position = new Vector3(rbBounce.position.x, 5, rbBounce.position.z);
            if (onAir == false)
            {
                rbBounce.transform.position = Vector3.MoveTowards(originalPosition, desiredPosition, Time.deltaTime * 6f);
                Debug.Log(originalPosition);
                rbBounce.transform.position = Vector3.MoveTowards(desiredPosition, originalPosition, Time.deltaTime * 0.4f);

                onAir = true;
            }

        }
    }
}