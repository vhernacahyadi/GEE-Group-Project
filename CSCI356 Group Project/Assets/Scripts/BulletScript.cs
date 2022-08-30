using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Destroy bullet after hitting a wall, floor or slime
    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.gameObject);
    }
}
