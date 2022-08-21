using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float point;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private float detectionRange;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float normalSpeed;

    private GameManager gameManager;
    private Animator animator;
    private GameObject player;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private GameObject[] escapePoints;
    private int currentEscapePoint;
    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        escapePoints = GameObject.FindGameObjectsWithTag("EscapePoint");
        agent.acceleration = 1000.0f;
        isRunning = false;

        if(escapePoints.Length == 0)
        {
            Debug.LogError("No escape points found in scene.");
        }

        SetEscapeDest();
        agent.SetDestination(escapePoints[currentEscapePoint].transform.position);

        // Audio
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;

        audioSource.clip = damageSound;

        audioSource.spatialBlend = 1.0f;
        audioSource.maxDistance = detectionRange;

        audioSource.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Damaged") == false && animator.GetBool("Dying") == false && player != null)
        {
            Vector3 playerToSlime = transform.position - player.transform.position;
            //Vector3 playerToSpawnPt = spawnPos - player.transform.position;

            // If player detected, run to an escape point far away
            if (playerToSlime.magnitude < detectionRange)
            {
                // If currently not running, find furthest escape point and set destination to it
                if(!isRunning)
                {
                    agent.speed = runSpeed;
                    SetEscapeDest();
                    agent.SetDestination(escapePoints[currentEscapePoint].transform.position);
                    isRunning = true;
                }

                // If currently running, only switch destination if current escape point is already reached
                else if (isRunning && Vector3.Distance(transform.position, escapePoints[currentEscapePoint].transform.position) <= 3.0f)
                {
                    agent.speed = runSpeed;
                    SetEscapeDest();
                    agent.SetDestination(escapePoints[currentEscapePoint].transform.position);
                }
            }

            // If player is far, patrol random escape point
            else
            {
                isRunning = false;
                agent.speed = normalSpeed;

                if (Vector3.Distance(transform.position, escapePoints[currentEscapePoint].transform.position) <= 5.0f)
                {
                    currentEscapePoint = Random.Range(0, escapePoints.Length);
                    agent.SetDestination(escapePoints[currentEscapePoint].transform.position);
                }
            }
        }
    }

    // Set currentEscapePoint to a new point
    private void SetEscapeDest()
    {
        float bestDistance = 0;
        int bestPoint = 0;
        for (int i = 0; i < escapePoints.Length; i++)
        {
            Vector3 playerToEscapePoint = escapePoints[i].transform.position - player.transform.position;
            float distance = playerToEscapePoint.magnitude;

            // Do not pick the same point as current
            if (distance > bestDistance && i != currentEscapePoint)
            {
                bestPoint = i;
                bestDistance = distance;
            }
        }

        currentEscapePoint = bestPoint;
    }

    private void MoveToSafeDirection()
    {
        bool isSafe = false;
        Vector3 dir = transform.forward;
        Vector3 newPos;
        Vector3 safestPos = transform.position;

        for (float rotation = 0; rotation < 360 && !isSafe; rotation += 10.0f)
        {
            dir = Quaternion.Euler(0, rotation, 0) * dir;
            newPos = transform.position + dir * 5.0f;

            // Shoot a Raycast out to the new direction, check if it hits something
            bool isHit = Physics.Raycast(transform.position, dir, out RaycastHit hit, 100.0f);

            // If the Raycast to the flee direction doesn't hit then the slime is good to go to this direction
            if (!isHit)
            {
                transform.Rotate(0, rotation, 0);
                agent.SetDestination(newPos);
                isSafe = true;
            }

            // Otherwise, check for max distance
            else if (Vector3.Distance(transform.position, hit.transform.position) > Vector3.Distance(transform.position, safestPos))
            {
                safestPos = hit.transform.position;
            }

        }

        // If cannot find a safe direction, go to direction with farthest obstacle
        if (!isSafe)
        {
            safestPos.y = 0;
            transform.LookAt(safestPos);
            agent.SetDestination(safestPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile" && animator.GetBool("Dying") == false && player != null)
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
            audioSource.Play();

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
