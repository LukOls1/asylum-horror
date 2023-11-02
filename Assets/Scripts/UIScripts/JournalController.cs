using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    [SerializeField] private GameObject journal;
    [SerializeField] private FirstPersonController playerController;

    private bool journalDisabled = false;

    private void Start()
    {
        SubscribeToNotesEvents();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            playerController.enabled = !playerController.enabled;
            journal.SetActive(!journalDisabled);
            journalDisabled = !journalDisabled;
        }
    }
    private void SubscribeToNotesEvents()
    {
        NoteObject[] notes = FindObjectsOfType<NoteObject>();
        foreach (NoteObject note in notes)
        {
            note.AddNoteEvent += HandleNote;
        }
    }
    private void HandleNote(string ID)
    {
        ShowNote(ID);
        AddNoteToList(ID);
    }
    private void AddNoteToList(string ID)
    {

    }
    private void ShowNote(string ID)
    {

    }
}
