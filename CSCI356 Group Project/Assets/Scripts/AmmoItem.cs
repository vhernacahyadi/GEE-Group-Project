using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoItem : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private AudioClip gunReloadClip;
     private AudioSource reloadSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        reloadSource = GetComponent<AudioSource>();
        reloadSource.clip = gunReloadClip;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;
        if (Input.GetKeyDown(KeyCode.Q))
            {
            Debug.Log(direction.magnitude);
            if (direction.magnitude < 1)
            {
                GameManager.bulletAmount += 6;
                //gunAudio.PlayOneShot(gunReloadClip);
                reloadSource.Play();
                Debug.Log("bullet amount left" + GameManager.bulletAmount);
                Destroy(gameObject);

            }
        }
    }
}
