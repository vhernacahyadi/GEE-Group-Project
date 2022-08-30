using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private const float MoveSpeed = 5.0f;
    //[SerializeField] private const float RotateSpeed = 20.0f;

    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private Transform GroundTester;
    [SerializeField] private LayerMask playerMask;
   
    private Animator playerAnimator;
    private AudioSource playerAudio;
    private bool isShooting = true;
    private bool isGrounded = true;
    private bool isJump = false;
    private Rigidbody rb;
    private bool onIce = false;
    private float mvX, mvZ;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mvX = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        mvZ = Input.GetAxis("Vertical") * Time.deltaTime * Speed;

        if (mvX != 0 || mvZ != 0)
        {
            // set trigger to run and play running audio
            if (isShooting)
            {
                playerAnimator.SetTrigger("run");
                isShooting = false;
                playerAudio.Play();
            }
        }
        else
        {
            // set trigger to shoot and stop running audio
            if (!isShooting)
            {
                playerAnimator.SetTrigger("shoot");
                isShooting = true;
                playerAudio.Stop();
            }
        }

        transform.Translate(mvX, 0, mvZ);
        //rb.AddForce((transform.forward * mvZ + transform.right * mvX).normalized * Speed * 20, ForceMode.Force);
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (onIce)
        {
            rb.AddForce((transform.forward * mvZ + transform.right * mvX).normalized * 0.2f, ForceMode.Impulse);
        }
        if (Physics.OverlapSphere(GroundTester.position, 0.2f, playerMask).Length == 0)
        {
            isGrounded = false;
            return;
        }
        else
        {
            isGrounded = true;
        }

        // make player jump
        if (isJump)
        {
            transform.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            isJump = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ice")
            onIce = true;
        else
            onIce = false;
    }
}
