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
    float delay, timer;
    //[SerializeField public Transform endMarker;
    void Start()
    {
        rbBounce = GameObject.FindWithTag("Bounce");
        Debug.Log("going bounce");
        originalPosition = rbBounce.transform.position;
        desiredPosition = new Vector3(rbBounce.transform.position.x, rbBounce.transform.position.y *2, rbBounce.transform.position.z);
        delay = 0.2f;
    }

    //private void OnTriggerEnter(Collider other)
    private void OnCollisionEnter(Collision other)
    {
        GameObject bouncer = other.gameObject;
        Rigidbody rb = bouncer.GetComponent<Rigidbody>();
        timer += Time.deltaTime;

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("on bounce");
            rb.AddForce(Vector3.up * bounceHelight);

            // rbBounce.transform.position = new Vector3(rbBounce.position.x, 5, rbBounce.position.z);
                rbBounce.transform.position = Vector3.MoveTowards(originalPosition, desiredPosition, 10 * Time.deltaTime );
                Debug.Log(originalPosition);

         }
    }
    void Update()
    {
        Debug.Log(timer);

        if (timer > delay)
        {
            Debug.Log("after 10 sec");
            //rbBounce.transform.position = Vector3.MoveTowards(desiredPosition, originalPosition, 10 * Time.deltaTime);
            rbBounce.transform.position = originalPosition * Time.deltaTime;
        }
    }
}