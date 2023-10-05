using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;
    private IInteractable actualInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && actualInteractable != null)
        {
            Debug.Log("used");
            actualInteractable.Interact();
            actualInteractable.ShowActionInfo();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {            
        if (collision.gameObject.tag == "Interactable")
        {
            actualInteractable = collision.gameObject.GetComponent<IInteractable>();
            interactText.text = actualInteractable.ShowActionInfo();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Interactable" )
        {
            Debug.Log("costamten");
            interactText.text = "";
            actualInteractable = null;
            
        }
    }
}
