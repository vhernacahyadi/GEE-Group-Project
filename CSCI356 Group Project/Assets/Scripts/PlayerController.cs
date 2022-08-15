using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private const float MoveSpeed = 5.0f;
    //[SerializeField] private const float RotateSpeed = 20.0f;

    [SerializeField] private float Speed = 5.0f;
    private Animator playerAnimator;
    private bool isShooting = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float mvX = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        float mvZ = Input.GetAxis("Vertical") * Time.deltaTime * Speed;

        if(mvX != 0 || mvZ != 0)
        {
            if (isShooting)
            {
                playerAnimator.SetTrigger("run");
                isShooting = false;
            }
        }
        else
        {
            if (!isShooting)
            {
                playerAnimator.SetTrigger("shoot");
                isShooting = true;
            }
        }

        transform.Translate(mvX, 0, mvZ);
    }
}
