using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {
    Animator doorAnim;
    bool doorOpen;

	// Use this for initialization
	void Start () {
        doorOpen = false;
        doorAnim = GetComponent<Animator>();
	}

    public void Open()
    {
        doorOpen = true;
        doorAnim.SetTrigger("Open");
    }

    public void Close()
    {
        if (doorOpen == true)
        {
            doorOpen = false;
            doorAnim.SetTrigger("Close");
        }
    }

}
