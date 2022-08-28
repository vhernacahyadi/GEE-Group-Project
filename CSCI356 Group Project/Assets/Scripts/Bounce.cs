using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bounceHelight = 400;
    private bool startTimer = false;
    GameObject rbBounce;
    Vector3 originalPosition;
    Vector3 desiredPosition;
    float delay, timer;

 
    void Start()
    {
        rbBounce = GameObject.FindWithTag("Bounce");
        Debug.Log("going bounce");
        originalPosition = rbBounce.transform.position;
        desiredPosition = new Vector3(rbBounce.transform.position.x, rbBounce.transform.position.y * 2, rbBounce.transform.position.z);
        delay = 0.1f;
    }

 
    private void OnCollisionEnter(Collision other)
    {
        GameObject bouncer = other.gameObject;
        Rigidbody rb = bouncer.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "Player")
        {
            rb.AddForce(Vector3.up * bounceHelight);
            startTimer = true;          
        }
        else
            startTimer = false;


    }
    void Update()
    {
        Debug.LogWarning(startTimer);
        // when timer starts, increment timer
        if (startTimer)
        {
            if(timer < delay)
            {
                Debug.LogWarning(timer);
                timer += Time.deltaTime;
                rbBounce.transform.position = Vector3.MoveTowards(rbBounce.transform.position, desiredPosition, 5 * Time.deltaTime);

            }
            else
            {
                Debug.LogWarning("yea");
                // Debug.LogWarning("after 10 sec");
                rbBounce.transform.position = Vector3.MoveTowards(rbBounce.transform.position, originalPosition, 5 * Time.deltaTime);
               

                
                // rbBounce.transform.position = originalPosition * Time.deltaTime;

            }

            if (rbBounce.transform.position == originalPosition)
            {
                Debug.LogWarning("yeaaaa");
                startTimer = false;
                timer = 0;
            }
        }

        

    }

}