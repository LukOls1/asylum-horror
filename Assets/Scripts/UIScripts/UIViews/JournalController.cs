using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalController : UIView
{
    [SerializeField] private GameObject journal;
    [SerializeField] private NotePopUp notePopUp;

    [SerializeField] private NoteData notesData;
    [SerializeField] private RectTransform notesButtonsContainer;
    [SerializeField] private Button noteUiPrefab;
    [SerializeField] private TextMeshProUGUI journalNoteContent;

    [SerializeField] private FirstPersonController playerController;

    private bool journalDisabled = false;

    private void Awake()
    {
        SubscribeNoteEvents();
    }
    private void Update()
    {
        //tego ifka przeniesc do osobnej klasy UIController
        if(Input.GetKeyDown(KeyCode.J))
        {
           //to bedzie metoda klasy UIController uzywana do wszystkich view spiêtych interfejsem IVIew
            
        }
    }
    private void SubscribeNoteEvents()
    {
        NoteObject.PassID += HandleNote;
    }
    public void HandleNote(string id)
    {
        int noteIndex = notesData.noteList.FindIndex(note => note.Id == id);
        Note note = notesData.noteList[noteIndex];
        notePopUp.ShowPopUp(note);

        Button noteButton = Instantiate(noteUiPrefab, notesButtonsContainer);
        NoteButton noteButtonScript = noteButton.GetComponent<NoteButton>();
        HandleNoteButton(noteButtonScript, note);
    }
    private void HandleNoteButton(NoteButton noteButtonScript, Note note)
    {
        noteButtonScript.note = note;
        noteButtonScript.SetHeader();
        noteButtonScript.journalNoteContent = journalNoteContent;
    }
}
