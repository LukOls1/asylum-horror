using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingField : MonoBehaviour, IInteractable
{
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private FieldOfView ghostFov;
    public enum FurnitureType
    {
        Bed,
        Desk
    }
    public FurnitureType furnitureType;

    private string actionInfo;
    private string hideInfo = "Hide";
    private string unHideInfo = "Stop Hiding";

    private void Start()
    {
        actionInfo = hideInfo;
        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        //ghostFov = GameObject.FindGameObjectWithTag("Ghost").GetComponent<FieldOfView>();
    }
    public void Interact()
    {
        if(ghostFov.PlayerSeen)
        {
            ghostFov.playerSeenHiding = true;
        }
        if(!playerController.IsHidden)
        {
            actionInfo = unHideInfo;
            playerController.Hide(this);
        }
        else if(playerController.IsHidden)
        {
            actionInfo = hideInfo;
            playerController.Hide(this);
        }        
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
