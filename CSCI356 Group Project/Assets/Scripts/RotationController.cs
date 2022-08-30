using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    //[SerializeField] private float HorizontalSens = 3.0f;
    //[SerializeField] private float VerticalSens = 2.0f;
    private GameObject character; 

    // Start is called before the first frame update
    void Start()
    {
        //disable mouse cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Assign a parent object
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseMove = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //Rotate the character horizontally
        character.transform.Rotate(0, mouseMove.x, 0);

        //Rotate the caamera vertically
        transform.Rotate(-mouseMove.y, 0, 0);   

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
