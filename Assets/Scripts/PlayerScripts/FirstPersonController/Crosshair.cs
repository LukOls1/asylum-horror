using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rayMaxDistance = 2f;
    private IInteractable actualInteractable;

    private void Update()
    {
        if(playerCamera != null)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = playerCamera.ScreenPointToRay(screenCenter);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, rayMaxDistance) &&  hit.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                actualInteractable = interactable;
                interactText.text = actualInteractable.ShowActionInfo();
                if (Input.GetKeyDown(KeyCode.E) && actualInteractable != null)
                {
                    Debug.Log("used");
                    actualInteractable.Interact();
                    actualInteractable.ShowActionInfo();
                }
            }
            else
            {
                interactText.text = "";
            }
        }
    }
}
