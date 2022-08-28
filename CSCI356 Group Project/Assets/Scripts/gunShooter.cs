using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunShooter : MonoBehaviour
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

                // If hitting target
                if (hit.transform.tag == "Target")
                {
                    if (SceneManager.GetActiveScene().name == "Level 2")
                    {
                        hit.transform.gameObject.GetComponent<SlimeAI>().Damage();
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponent<SlimeController>().Damage();
                    }
                }
            }

            //Spawn bullet
            gunAudio.PlayOneShot(gunShootClip);
            GameObject newBulletPrefab = GameObject.Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
            newBulletPrefab.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward * bulletForce, ForceMode.Impulse);


        }
    }


}
