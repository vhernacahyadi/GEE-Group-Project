using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletForce = 200f;
    [SerializeField] private AudioClip gunShootClip;

    private RaycastHit hitInfo;
    private AudioSource gunAudio;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            //Vector3 directionOfFire = playerCamera.transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
            {
                spawnPoint.transform.LookAt(hit.point);
            }


            //Spawn bullet
            gunAudio.PlayOneShot(gunShootClip);
            GameObject newBulletPrefab = GameObject.Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
            newBulletPrefab.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward * bulletForce, ForceMode.Impulse);
            

        }
    }

    
}
