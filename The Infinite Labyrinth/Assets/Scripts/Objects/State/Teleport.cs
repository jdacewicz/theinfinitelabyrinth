using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactable
{
    public float distanceOfNextTeleport;

    public AudioClip lockedDoor;

    public RoomController roomController;
    private AudioSource audioSource;

    private void Start()
    {      
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1.5f;
    }

    private void Update()
    {
        roomController = GetComponentInParent<RoomController>();
        Debug.DrawRay(transform.position, transform.forward * distanceOfNextTeleport, Color.green);
    }

    public override void Interact()
    {
        if (roomController.currentRoom.enemysAmmount > 0)
        {
            audioSource.PlayOneShot(lockedDoor);   
        } 
        else
        {
            StartCoroutine(TeleportToRoom());
        }       
    }

    private IEnumerator TeleportToRoom()
    {
        RaycastHit hit;

        var directionOfTeleportCheck = new Dictionary<string, string>
        {
            {"TopDoor", "Bottom" },
            {"BottomDoor", "Top" },
            {"LeftDoor", "Right" },
            {"RightDoor", "Left" }
        };

        string acceptedDirectionOfPortal = directionOfTeleportCheck[gameObject.transform.name];

        int layerMask = LayerMask.GetMask(acceptedDirectionOfPortal.Replace("Door", "") + "Portal");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceOfNextTeleport, layerMask))
        {          
            audioSource.Play();

            yield return new WaitForSeconds(0.1f);

            Vector3 teleportedPosition = hit.transform.position + (hit.transform.forward * -1) * 0.1f;
            TeleportToCoordinates(GameObject.FindWithTag("Player"), teleportedPosition.x, teleportedPosition.z);       
        }
    }

    public void TeleportToCoordinates(GameObject teleportedObject, float x, float z)
    {
        teleportedObject.transform.position = new Vector3(x, 0.2f, z);
    }
}
