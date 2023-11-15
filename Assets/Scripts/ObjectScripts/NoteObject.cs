using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject: MonoBehaviour, IInteractable
{

    [SerializeField] private string actionInfo = "Note";
    [SerializeField] private string noteID;

    //[SerializeField] private JournalController journalController;
    //[SerializeField] private NoteData noteData;

    public static event Action<string> PassID;

    private void Awake()
    {
        //jesli obiekt note nie zawiara sie na liscie, to wyjeb loga
    }

    public void Interact()
    {
        //journalController.HandleNote(noteID);
        PassID?.Invoke(noteID);
        Destroy(gameObject);
        
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
    //funkcja pokazuj¹ca notatke do przeczytania po podniesieniu 
}
