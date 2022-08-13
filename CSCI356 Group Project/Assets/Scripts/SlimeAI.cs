using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [SerializeField]
    private float detectionRange = 1.0f;

    [SerializeField]
    private float upForce = 1.0f;

    private GameObject player;
    private Rigidbody rb;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If player detected within detectionRange, run
        Vector3 vectorToSlime = transform.position - player.transform.position;

        Debug.Log(vectorToSlime.magnitude);

        if (vectorToSlime.magnitude < detectionRange && !isJumping)
        {
            vectorToSlime.y = 0;
            transform.rotation = Quaternion.LookRotation(vectorToSlime);

            Vector3 jump = vectorToSlime.normalized;
            jump.y = upForce;
            rb.AddForce(jump, ForceMode.Impulse);

            Debug.Log(jump);

            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isJumping && collision.collider.tag == "Ground")
        {
            Debug.Log("COllide with ground");

            isJumping = false;
        }
    }
}
