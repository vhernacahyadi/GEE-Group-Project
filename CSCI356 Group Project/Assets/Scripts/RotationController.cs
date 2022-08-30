using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    private GameObject character;
    private float verticalRotation;

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
        verticalRotation = transform.rotation.eulerAngles.x;
        Vector2 mouseMove = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //Rotate the character horizontally
        character.transform.Rotate(0, mouseMove.x, 0);

        //Rotate the caamera vertically
        if (transform.rotation.eulerAngles.x <= 40 || transform.rotation.eulerAngles.x >= 310)
            transform.Rotate(-mouseMove.y, 0, 0);
        else if(transform.rotation.eulerAngles.x > 40 && transform.rotation.eulerAngles.x < 100)
            transform.rotation = Quaternion.Euler(39.9f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        else if (transform.rotation.eulerAngles.x > 100 && transform.rotation.eulerAngles.x < 310)
            transform.rotation = Quaternion.Euler(310.1f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
