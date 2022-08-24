// LIGHT SCRIPT
using UnityEngine;

public class InteractButton : Interactable
{
    private RaycastHit hitInfo;
    public GameObject Door;

    public override string GetDescription()
    {
        return "Press [E] to INTERACT";
    }

    public override void Interact()
    {
        Debug.Log("INTERACT!");

        // After click here
        Destroy(Door);
        
    }
}