using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float point;

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

        audioSource.volume = 0.1f;
        audioSource2.volume = 0.1f;

    }

    // Update is called once per frame
    void Update()
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

                Vector3 jump = transform.forward.normalized * runSpeed;
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

                Vector3 jump = transform.forward.normalized;
                if (toSpawnPoint.magnitude > runSpeed)
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

                Vector3 jump = transform.forward.normalized;
                jump.y = upForce;
                rb.AddForce(jump, ForceMode.Impulse);

                isJumping = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isJumping && collision.collider.CompareTag("Boundary"))
        {
            // Play jump sound
            audioSource.Play();

            // Turn around
            transform.Rotate(0, 180, 0, Space.Self);
            transform.Rotate(0, Random.Range(-90, 90), 0, Space.Self); // add some randomness

            Vector3 jump = transform.forward.normalized * runSpeed;
            jump.y = 0;
            rb.AddForce(jump, ForceMode.Impulse);

            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isJumping && collision.collider.tag == "Boundary")
        {
            // Play jump sound
            audioSource.Play();

            // Turn around
            transform.LookAt(collision.contacts[0].normal);
            transform.Rotate(0, Random.Range(-90, 90), 0, Space.Self); // add some randomness

            Vector3 jump = transform.forward.normalized * runSpeed;
            jump.y = upForce;
            rb.AddForce(jump, ForceMode.Impulse);

            isJumping = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile" && animator.GetBool("Dying") == false && player !=null)
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
