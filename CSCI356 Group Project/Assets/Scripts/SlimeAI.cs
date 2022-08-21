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

        if (escapePoints.Length == 0)
        {
            Debug.LogError("No escape points found in scene.");
        }

        SetEscapeDest();

        // Randomly move to random direction
        StartCoroutine(RandomDirection());

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
                if (!isRunning)
                {
                    agent.speed = runSpeed;
                    SetEscapeDest();

                    isRunning = true;
                }

                // If currently running, switch destination if current escape point is already reached
                else if (isRunning && (Vector3.Distance(transform.position, agent.destination) <= 3.0f))
                {
                    agent.speed = runSpeed;
                    SetEscapeDest();
                }

            }
            // If player is far, go to random escape point
            else
            {
                isRunning = false;
                agent.speed = normalSpeed;

                if (Vector3.Distance(transform.position, agent.destination) <= 3.0f)
                {
                    agent.SetDestination(escapePoints[Random.Range(0, escapePoints.Length)].transform.position);
                }
            }
        }
    }

    // Set currentEscapePoint to a new point
    private void SetEscapeDest()
    {
        float bestDistance = 0;
        int bestPoint = 0;

        // Loop through escape points and pick farthest
        for (int i = 0; i < escapePoints.Length; i++)
        {
            Vector3 playerToEscapePoint = escapePoints[i].transform.position - player.transform.position;
            float distance = playerToEscapePoint.magnitude;

            // Do not pick the same point as current
            if (distance > bestDistance && escapePoints[i].transform.position != agent.destination)
            {
                bestPoint = i;
                bestDistance = distance;
            }
        }

        agent.SetDestination(escapePoints[bestPoint].transform.position);
    }

    // Randomly steer to random point
    private IEnumerator RandomDirection()
    {
        bool isOffCourse = false;
        Vector3 currentEscapePoint = agent.destination;

        while (true)
        { 
            if (!isOffCourse)
            {
                // Store the correct destination
                currentEscapePoint = agent.destination;

                // Set random
                transform.Rotate(0, Random.Range(-90, 90), 0);
                agent.SetDestination(transform.position + transform.forward * 6.0f);

                isOffCourse = true;
            }
            else
            {
                // Set to proper destination
                agent.SetDestination(currentEscapePoint);

                isOffCourse= false;
            }

            yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
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
