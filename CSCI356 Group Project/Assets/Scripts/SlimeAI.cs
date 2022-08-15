using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [SerializeField]
    private float detectionRange = 1.0f;

    [SerializeField]
    private float upForce = 1.0f;

    [SerializeField]
    private float runSpeed = 1.0f;

    private Animator animator;
    private GameObject player;
    private Rigidbody rb;

    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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
            vectorToSlime.y = 0;
            transform.rotation = Quaternion.LookRotation(vectorToSlime);
            transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(-90, 90));

            Vector3 jump = transform.forward.normalized * runSpeed;
            jump.y = upForce;
            rb.AddForce(jump, ForceMode.Impulse);

            isJumping = true;
        }

        // Idle move
        else if (isJumping == false && animator.GetBool("Damaged") == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));

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
