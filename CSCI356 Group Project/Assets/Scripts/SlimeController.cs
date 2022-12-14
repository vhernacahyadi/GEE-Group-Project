using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private int point;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private float detectionRange;

    [SerializeField]
    private float upForce;

    [SerializeField]
    private float runSpeed;

    private Animator animator;
    private GameManager gameManager;
    private GameObject player;
    private Rigidbody rb;
    private Collider col;
    private AudioSource audioSource;
    private AudioSource audioSource2;

    private bool isJumping;
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        spawnPos = transform.position;
        isJumping = true;

        // Audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource2.playOnAwake = false;

        audioSource.clip = jumpSound;
        audioSource2.clip = damageSound;

        audioSource.spatialBlend = 1.0f;
        audioSource.maxDistance = detectionRange;
    }

    void FixedUpdate()
    {
        if (isJumping == false && animator.GetBool("Damaged") == false && animator.GetBool("Dying") == false && player != null)
        {
            Vector3 playerToSlime = transform.position - player.transform.position;
            Vector3 playerToSpawnPt = spawnPos - player.transform.position;

            // If player detected within detectionRange, run
            if (playerToSlime.magnitude < detectionRange)
            {
                // Play jump sound
                audioSource.Play();

                playerToSlime.y = 0;
                transform.rotation = Quaternion.LookRotation(playerToSlime); // face away from player
                transform.Rotate(0, Random.Range(-20, 20), 0, Space.Self); // add some randomness

                // Jump
                Vector3 jump = transform.forward * runSpeed;
                jump.y = upForce;
                rb.AddForce(jump, ForceMode.Impulse);

                isJumping = true;
            }

            // If player is far from slime spawn point, go back to spawn point
            else if (playerToSpawnPt.magnitude > detectionRange)
            {
                // Play jump sound
                audioSource.Play();

                Vector3 toSpawnPoint = spawnPos - transform.position;
                toSpawnPoint.y = 0;

                if (toSpawnPoint != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(toSpawnPoint); // face spawnPos direction
                }
                transform.Rotate(0, Random.Range(-20, 20), 0, Space.Self); // add some randomness

                // Jump
                Vector3 jump = transform.forward;
                if (toSpawnPoint.magnitude > runSpeed) // if spawn point is far, add speed
                    jump *= runSpeed;
                jump.y = upForce;
                rb.AddForce(jump, ForceMode.Impulse);

                isJumping = true;
            }

            // Idle movement
            else
            {
                // Play jump sound
                audioSource.Play();

                transform.Rotate(0, Random.Range(0, 360), 0, Space.Self); // random orientation

                // Jump
                Vector3 jump = transform.forward;
                jump.y = upForce;
                rb.AddForce(jump, ForceMode.Impulse);

                isJumping = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Jump again if collide with wall or other slimes
        if (collision.collider.CompareTag("Boundary") || collision.collider.CompareTag("Target"))
        {
            // Play jump sound
            audioSource.Play();

            // Turn around
            Vector3 contactToSlime = transform.position - collision.contacts[0].point;
            contactToSlime.y = 0;
            transform.rotation = Quaternion.LookRotation(contactToSlime);
            transform.Rotate(0, Random.Range(-90, 90), 0, Space.Self); // add some randomness

            Vector3 playerToSlime = transform.position - player.transform.position;

            // Jump
            Vector3 jump = playerToSlime.magnitude < detectionRange ? transform.forward * runSpeed : transform.forward;
            jump.y = isJumping ? 0 : 1.0f;
            rb.AddForce(jump, ForceMode.Impulse);

            isJumping = true;
        }
        else if (collision.collider.CompareTag("Ground"))
        {
            // Set isJumping to false to trigger jump on next Update
            isJumping = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Play jump sound
        audioSource.Play();

        // Turn around
        Vector3 contactToSlime = transform.position - collision.contacts[0].point;
        contactToSlime.y = 0;
        if (contactToSlime != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(contactToSlime);
        transform.Rotate(0, Random.Range(-90, 90), 0, Space.Self); // add some randomness

        // Jump
        Vector3 jump = transform.forward;
        jump.y = 1.0f;
        rb.AddForce(jump, ForceMode.Impulse);

        isJumping = true;
    }

    public void Damage()
    {
        // Face the player
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        vectorToPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(vectorToPlayer);

        // Reduce health
        health--;

        if (health == 0)
        {
            // Increment the score in the GameManager when the slime is damaged
            if (gameManager != null)
            {
                gameManager.AddScore(point);
            }

            // Play damaged sound
            audioSource2.Play();

            // Play dying animation
            animator.SetBool("Dying", true);
            col.enabled = false;
        }
        else
        {
            // Play damage animation
            animator.SetBool("Damaged", true);
        }

    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

    public void OnDamagedAnimationFinished()
    {
        animator.SetBool("Damaged", false);
    }

}
