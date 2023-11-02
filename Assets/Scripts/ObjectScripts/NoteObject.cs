using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour,IInteractable
{

    private string actionInfo = "Note";
    [SerializeField] private string noteID = "";

    public delegate void AddNoteHandler(string ID);
    public event AddNoteHandler AddNoteEvent;

    public void Interact()
    {
        SendNoteID(noteID);
        Destroy(gameObject);
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
    private void SendNoteID(string ID)
    {
        AddNoteEvent(ID);
    }
}
