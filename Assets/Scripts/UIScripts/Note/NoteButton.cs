using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteButton : MonoBehaviour
{
    public TextMeshProUGUI journalNoteContent;
    public Note note = new Note();

    [SerializeField] private TextMeshProUGUI buttonText;

    public void ShowNoteContent_OnClick()
    {
        journalNoteContent.text = note.Content;
    }
    public void SetHeader()
    {
        buttonText.text = note.header;
    }
}
