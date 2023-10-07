using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadNumber : MonoBehaviour,IInteractable
{
    private string numberName;
    private string actionInfo;
    private Keypad keypad;
    private void Start()
    {
        numberName = transform.parent.name;
        actionInfo = numberName;
        keypad = transform.parent.GetComponentInParent<Keypad>();
    }
    public void Interact()
    {
        keypad.GetNumber(numberName);
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
