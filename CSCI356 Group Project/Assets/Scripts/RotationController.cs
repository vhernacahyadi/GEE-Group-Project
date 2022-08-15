using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private float HorizontalSens = 3.0f;
    [SerializeField] private float VerticalSens = 2.0f;
    private GameObject character;   //the class GameObject is G and O capital

    // Start is called before the first frame update
    void Start()
    {
        //disable mouse cursor
        Cursor.lockState = CursorLockMode.Locked;

        //destroys object
        //this.gameObject.Destroy();  //when accessing a game object's property, is small g and capital O

        //Assign a parent object
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //dont use var, not good programming practice in C#
        Vector2 mouseMove = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //Rotate the character horizontally
        character.transform.Rotate(0, mouseMove.x, 0);
        transform.Rotate(-mouseMove.y, 0, 0);   //Rotate the caamera vertically

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
