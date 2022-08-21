// LIGHT SCRIPT
using UnityEngine;

public class InteractCannon : Interactable
{
    private AudioSource gunAudio;
    private RaycastHit hitInfo;

    private void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    public override string GetDescription()
    {
        return "Press [E] to INTERACT";
    }

    public override void Interact()
    {
        Debug.Log("INTERACT!");
        gunAudio.Play();

        Vector3 directiononOfFire = transform.up; // because the gun barrel (cylinder) has been rotated from vertical to horizontal

        if (Physics.Raycast(transform.position, directiononOfFire, out hitInfo, 100))
        {
            // Send a message to the object corresponding to hitInfo
            hitInfo.transform.SendMessage("HitByExplosiveShell", directiononOfFire, SendMessageOptions.DontRequireReceiver);
        }

        // Show a line of fire in  the scene view. Disable after debugging
        Debug.DrawLine(transform.position, hitInfo.point, Color.yellow);
    }
}