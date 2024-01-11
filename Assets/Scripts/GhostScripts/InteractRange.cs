using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRange : MonoBehaviour
{
    public bool PlayerCought = false;
    public GameObject killCamera;
    [SerializeField] private FirstPersonController playerController;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent<Door>(out Door door) && door.doorState == Door.DoorState.Closed)
        {
            door.Interact();
        }
        else if(other.CompareTag("Player"))
        {
            if (!playerController.IsHidden)
            {
                PlayerCought = true;
                killCamera.SetActive(true);
            }
        }
    }
}
