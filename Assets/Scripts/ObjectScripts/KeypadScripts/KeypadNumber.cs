using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadNumber : MonoBehaviour,IInteractable
{
    private string actionInfo = "Push";
    public void Interact()
    {

    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
