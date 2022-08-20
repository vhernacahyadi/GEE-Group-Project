using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player near ammo");

            if (TryGetComponent(out gunShooter gunShooter))
            {
                Debug.Log(gunShooter.bulletAmount);

                gunShooter.bulletAmount += 6;
                Debug.Log("gunShooter.bulletAmount after" + gunShooter.bulletAmount);
                Destroy(gameObject);
            }
        }
    }

}
