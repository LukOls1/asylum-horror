using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Hideable, IInteractable
{
    private FirstPersonController playerController;
    private string actionInfo;
    private string hideInfo = "Hide";
    private string unHideInfo = "Stop Hiding";

    private void Start()
    {
        actionInfo = hideInfo;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }
    public void Interact()
    {  
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
