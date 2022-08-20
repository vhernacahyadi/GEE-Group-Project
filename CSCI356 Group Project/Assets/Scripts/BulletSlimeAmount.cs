using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSlimeAmount : MonoBehaviour
{
    public GameObject player;
    public Text bulletText;
    public Text slimeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text ="Bullet Left:" +"\n"+ GameManager.bulletAmount.ToString();
        slimeText.text = "Slime left:" + "\n" + GameManager.maxSpawn.ToString();
    }
}
