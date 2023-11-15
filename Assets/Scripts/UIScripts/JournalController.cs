using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalController : MonoBehaviour
{
    [SerializeField] private GameObject journal;

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
    private void SubscribeNoteEvents()
    {
        NoteObject.PassID += HandleNote;
    }
    public void HandleNote(string id)
    {
        int noteIndex = notesData.noteList.FindIndex(note => note.Id == id);
        if (noteIndex != -1)
        {
            Button noteButton = Instantiate(noteUiPrefab, notesButtonsContainer);
            NoteButton noteButtonScript = noteButton.GetComponent<NoteButton>();
            noteButtonScript.note = notesData.noteList[noteIndex];
            noteButtonScript.journalNoteContent = journalNoteContent;
        }
        else throw new ArgumentException("This note is not on Notes Data list", nameof(noteIndex));
    }
}
