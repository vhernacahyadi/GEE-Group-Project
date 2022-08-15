using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [SerializeField]
    private float detectionRange = 1.0f;

    [SerializeField]
    private float upForce = 1.0f;

    private Animator animator;
    private GameObject player;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player detected within detectionRange, run
        Vector3 vectorToSlime = transform.position - player.transform.position;

        if (animator.GetBool("Jump") == false &&
            animator.GetBool("Damaged") == false &&
            vectorToSlime.magnitude < detectionRange)
        {
            vectorToSlime.y = 0;
            transform.rotation = Quaternion.LookRotation(vectorToSlime);

            Vector3 jump = vectorToSlime.normalized;
            jump.y = upForce;
            rb.AddForce(jump, ForceMode.Impulse);

            animator.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (animator.GetBool("Jump") && collision.collider.tag == "Ground")
        {
            animator.SetBool("Jump", false);
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
        // Play damage animation
        animator.SetBool("Damaged", true);
    }

    public void OnDeathAnimationFinished()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }

}
