using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Camera playerCamera;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletForce = 10f;

    private RaycastHit hitInfo;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        const int damage = 1;

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 directionOfFire = playerCamera.transform.forward;

            if(Physics.Raycast(transform.position, directionOfFire, out hitInfo, 20))
            {
                //target damage calculations goes here
            }

            //Spawn bullet
            GameObject newBulletPrefab = GameObject.Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
            newBulletPrefab.GetComponent<Rigidbody>().AddForce(directionOfFire * bulletForce, ForceMode.Impulse);
        }
    }
}
