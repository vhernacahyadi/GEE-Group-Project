using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour
{
    [SerializeField]
    private float health = 3;

    [SerializeField]
    private int point = 5;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private float detectionRange = 10.0f;

    [SerializeField]
    private float runSpeed = 5.0f;

    [SerializeField]
    private float normalSpeed = 2.0f;

    [SerializeField]
    private float offCourseInterval = 0.1f;

    [SerializeField]
    private float onCourseInterval = 2.0f;

    private GameManager gameManager;
    private Animator animator;
    private GameObject player;
    private AudioSource audioSource;
    private NavMeshAgent agent;
    private GameObject[] escapePoints;
    private Vector3 currentEscapePoint;
    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        escapePoints = GameObject.FindGameObjectsWithTag("EscapePoint");
        agent.acceleration = 10000.0f;
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

        //audioSource.volume = 0.1f;
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

                // If currently running, switch destination if current destination is already reached
                else if (isRunning && (Vector3.Distance(transform.position, currentEscapePoint) <= 3.0f))
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

                // Change escape point if already reached
                if (Vector3.Distance(transform.position, currentEscapePoint) <= 3.0f)
                {
                    currentEscapePoint = escapePoints[Random.Range(0, escapePoints.Length)].transform.position;
                    agent.SetDestination(currentEscapePoint);
                }
            }

        }
    }

    // Set currentEscapePoint to a new point
    private void SetEscapeDest()
    {
        List<int> validEscapePoints = new List<int>();

        // Loop through escape points and check if it is far enough from player
        for (int i = 0; i < escapePoints.Length; i++)
        {
            Vector3 playerToEscapePoint = escapePoints[i].transform.position - player.transform.position;
            float distance = playerToEscapePoint.magnitude;

            // Do not pick the same point as current
            if (distance > detectionRange && escapePoints[i].transform.position != currentEscapePoint)
            {
                validEscapePoints.Add(i);
            }
        }

        // Randomly select escape points that are outside detection range
        if (validEscapePoints.Count > 0)
        {
            int randIdx = Random.Range(0, validEscapePoints.Count);
            agent.SetDestination(escapePoints[validEscapePoints[randIdx]].transform.position);
        }
        // If no escape point is far enough, select random
        else
        {
            int randIdx = Random.Range(0, escapePoints.Length);
            agent.SetDestination(escapePoints[randIdx].transform.position);
        }

        currentEscapePoint = agent.destination;
    }

    // Randomly steer to random point
    private IEnumerator RandomDirection()
    {
        bool isOffCourse = false;

        while (true)
        {
            if (!isOffCourse)
            {
                // Set random
                transform.Rotate(0, Random.Range(-80, 80), 0);

                RaycastHit hitInfo;
                bool isHit = Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance: 20.0f);

                if (isHit)
                {
                    agent.SetDestination(hitInfo.point);
                    //Debug.DrawLine(transform.position, agent.destination, Color.green, duration: 1.0f);
                }
                else
                {
                    agent.SetDestination(transform.position + transform.forward * 5.0f);
                }

                isOffCourse = true;

                if (isRunning)
                    yield return new WaitForSeconds(offCourseInterval);
                else
                    yield return new WaitForSeconds(offCourseInterval * runSpeed / normalSpeed);
            }
            else
            {
                // Set to proper destination
                agent.SetDestination(currentEscapePoint);

                isOffCourse = false;
                yield return new WaitForSeconds(onCourseInterval);
            }

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
