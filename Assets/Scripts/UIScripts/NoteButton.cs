using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteButton : MonoBehaviour
{
    public TextMeshProUGUI journalNoteContent;
    public Note note = new Note();

    public void ShowNoteContent_OnClick()
    {
        Debug.Log(note.Content + "   " + journalNoteContent.ToString());
        journalNoteContent.text = note.Content;
    }
}
