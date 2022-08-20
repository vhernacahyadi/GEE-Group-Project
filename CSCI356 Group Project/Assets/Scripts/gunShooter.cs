using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletForce = 10f;
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
        const int damage = 1;

        if (Input.GetButtonDown("Fire1")&& GameManager.bulletAmount>0)
        {
            Vector3 directionOfFire = playerCamera.transform.forward;

            if(Physics.Raycast(transform.position, directionOfFire, out hitInfo, 20))
            {
                //target damage calculations goes here
            }
           // Debug.Log("bulletAmount b4:" + bulletAmount);

            //Spawn bullet
            gunAudio.PlayOneShot(gunShootClip);
            GameObject newBulletPrefab = GameObject.Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
            newBulletPrefab.GetComponent<Rigidbody>().AddForce(directionOfFire * bulletForce, ForceMode.Impulse);
            GameManager.bulletAmount -= 1;
           Debug.Log("bullet left:"+ GameManager.bulletAmount);

        }
    }

    
}
