// LIGHT SCRIPT
using UnityEngine;

public class InteractBouncing : Interactable
{
    private AudioSource gunAudio;
    private RaycastHit hitInfo;

    public override string GetDescription()
    {
        return "Jump here";
    }
    public override void Interact()
    {
        Debug.Log("INTERACT!");



    }

}