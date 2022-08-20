using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoItem : MonoBehaviour
{
    private GameObject player;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
                Debug.Log("Player near ammo");
                Debug.Log(GameManager.bulletAmount);

                 GameManager.bulletAmount += 6;
                 Debug.Log("gunShooter.bullet amount left" + GameManager.bulletAmount);
                  Destroy(gameObject);

            }
        }
    }
    //private void OnCollisionEnter(Collision cls)
    //{
    //    Debug.Log("OnCollisionEnter");
    //    if (cls.gameObject.tag.Equals("Player"))
    //    {
    //        Debug.Log("Player OnCollisionEnter");

    //        if (TryGetComponent(out gunShooter gunShooter))
    //        {
    //            Debug.Log(gunShooter.bulletAmount);

    //            gunShooter.bulletAmount += 6;
    //            Debug.Log("gunShooter.bulletAmount after" + gunShooter.bulletAmount);
    //            Destroy(gameObject);
    //        }
    //    }
    //}

}
