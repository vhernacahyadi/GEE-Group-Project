using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private float detectionRange = 1.0f;

    [SerializeField]
    private float upForce = 1.0f;

    [SerializeField]
    private float runSpeed = 1.0f;

    private Animator animator;
    private GameObject player;
    private Rigidbody rb;
    private AudioSource audioSource;
    private AudioSource audioSource2;

    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();

        audioSource.clip = jumpSound;
        audioSource2.clip = damageSound;

        audioSource.volume = 0.2f;
        audioSource2.volume = 0.2f;

        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If player detected within detectionRange, run
        Vector3 vectorToSlime = transform.position - player.transform.position;
        if (isJumping == false && animator.GetBool("Damaged") == false &&
            vectorToSlime.magnitude < detectionRange)
        {
            // Play jump sound
            audioSource.Play();

            vectorToSlime.y = 0;
            transform.rotation = Quaternion.LookRotation(vectorToSlime);
            transform.Rotate(0, Random.Range(-20, 20), 0, Space.Self);

            Vector3 jump = transform.forward.normalized * runSpeed;
            jump.y = upForce;
            rb.AddForce(jump, ForceMode.Impulse);

            isJumping = true;
        }

        // Idle move
        else if (isJumping == false && animator.GetBool("Damaged") == false)
        {
            transform.Rotate(0, Random.Range(0,90), 0, Space.Self);

            Vector3 jump = transform.forward.normalized;
            jump.y = upForce;
            rb.AddForce(jump, ForceMode.Impulse);

            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isJumping && collision.collider.tag == "Ground")
        {
            isJumping = false;
        }

        if (collision.collider.tag == "Projectile")
        {
            // Face the player
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            vectorToPlayer.y = 0;
            transform.rotation = Quaternion.LookRotation(vectorToPlayer);

            Damage();
        }

    }
    
    private void OnCollisionStay(Collision collision)
    {

        if (!isJumping && collision.collider.tag == "Boundary")
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);

            Vector3 jump = transform.forward.normalized * runSpeed;
            jump.y = upForce;
            rb.AddForce(jump, ForceMode.Impulse);

            isJumping = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            // Face the player
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            vectorToPlayer.y = 0;
            transform.rotation = Quaternion.LookRotation(vectorToPlayer);

            Damage();
        }

    }

    public void Damage()
    {
        // Play damaged sound
        audioSource2.Play();

        // Play damage animation
        animator.SetBool("Damaged", true);
    }

    public void OnDeathAnimationFinished()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }

}
