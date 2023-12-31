using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRange : MonoBehaviour
{
    public bool PlayerCought = false;
    public GameObject killCamera;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent<Door>(out Door door) && door.doorState == Door.DoorState.Closed)
        {
            door.Interact();
        }
        else if(other.CompareTag("Player"))
        {
            PlayerCought = true;
            killCamera.SetActive(true);
        }
    }
}
