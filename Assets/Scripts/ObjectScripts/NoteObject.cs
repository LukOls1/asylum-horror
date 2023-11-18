using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject: MonoBehaviour, IInteractable
{

    [SerializeField] private string actionInfo = "Note";
    [SerializeField] private string noteID;

    public static event Action<string> PassID;

    public void Interact()
    {
        PassID?.Invoke(noteID);
        Destroy(gameObject);    
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
