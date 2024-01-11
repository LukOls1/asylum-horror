using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private float rayMaxDistance = 2f;
    [SerializeField] private FirstPersonController playerController;

    private IInteractable actualInteractable;

    private void Update()
    {
        if(PlayerCamera != null)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = PlayerCamera.ScreenPointToRay(screenCenter);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, rayMaxDistance) &&  hit.transform.TryGetComponent<IInteractable>(out IInteractable interactable) && !playerController.IsHidden)
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                actualInteractable = interactable;
                interactText.text = actualInteractable.ShowActionInfo();
                if (Input.GetKeyDown(KeyCode.E) && actualInteractable != null)
                {
                    actualInteractable.Interact();
                    actualInteractable.ShowActionInfo();
                }
            }
            else if(playerController.IsHidden)
            {
                interactText.text = actualInteractable.ShowActionInfo();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    actualInteractable.Interact();
                }
            }
            else
            {
                interactText.text = "";
            }
        }
        else
        {
            interactText.text = "";
        }
    }
    private void OnDisable()
    {
        interactText.text = "";
    }
}
