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

    private Vector3 spawnPos;
    private GameManager gameManager;
    private Animator animator;
    private GameObject player;
    private AudioSource audioSource;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        spawnPos = transform.position;

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
            Vector3 playerToSpawnPt = spawnPos - player.transform.position;

            // If player detected within detectionRange, run
            if (playerToSlime.magnitude < detectionRange)
            {
                Vector3 dest = transform.position + playerToSlime;
                agent.SetDestination(dest);
            }

            // If player is far from slime spawn point, go back to spawn point
            else if (playerToSpawnPt.magnitude > detectionRange)
            {
                // Get random point around the spawn point
                Vector3 randomPos = Random.insideUnitCircle;
                randomPos = transform.position + randomPos;
                agent.SetDestination(randomPos);
            }

            // Idle movement
            else
            {
                // Do sth
            }
        }
    }

    private void Run()
    {
        //We will check if enemy can flee to the direction opposite from the player, we will check if there are obstacles
        bool isDirSafe = false;

        //We will need to rotate the direction away from the player if straight to the opposite of the player is a wall
        float vRotation = 0;

        while (!isDirSafe)
        {
            //Calculate the vector pointing from Player to the Enemy
            Vector3 dirToPlayer = transform.position - player.transform.position;

            //Calculate the vector from the Enemy to the direction away from the Player the new point
            Vector3 newPos = transform.position + dirToPlayer;

            //Rotate the direction of the Enemy to move
            newPos = Quaternion.Euler(0, vRotation, 0) * newPos;

            //Shoot a Raycast out to the new direction with 5f length (as example raycast length) and see if it hits an obstacle
            bool isHit = Physics.Raycast(transform.position, newPos, out RaycastHit hit);

            if (hit.transform == null)
            {
                //If the Raycast to the flee direction doesn't hit a wall then the Enemy is good to go to this direction
                agent.SetDestination(newPos);
                isDirSafe = true;
            }

            //Change the direction of fleeing is it hits a wall by 20 degrees
            if (isHit && hit.transform.CompareTag("Boundary"))
            {
                vRotation += 20;
                isDirSafe = false;
            }
            else
            {
                //If the Raycast to the flee direction doesn't hit a wall then the Enemy is good to go to this direction
                agent.SetDestination(newPos);
                isDirSafe = true;
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
