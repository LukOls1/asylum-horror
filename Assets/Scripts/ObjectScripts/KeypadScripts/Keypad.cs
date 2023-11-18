using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    private string validCode;
    private string typedCode;
    [SerializeField] private NoteData noteData;
    [SerializeField] private string noteID;

    [SerializeField] private int codeLength = 4;
    [SerializeField] private RandomizeCode randomizeCode;

    [SerializeField] private Door controlledDoor;

    private void Start()
    {
        controlledDoor.doorState = Door.DoorState.Locked;
        validCode = randomizeCode.GenerateCode(codeLength);
        PassCodeToNote(validCode, noteID, noteData);
    }

    public void GetNumber(string number)
    {
        typedCode += number;
        if(typedCode.Length == validCode.Length)
        {
            CheckCode();
        }
    }
    private void CheckCode()
    {
        if(typedCode == validCode)
        {
            controlledDoor.doorState = Door.DoorState.Closed;
        }
        else
        {
            typedCode = "";
        }
    }
    private void PassCodeToNote(string code, string id, NoteData noteData)
    {
        int index = noteData.noteList.FindIndex(note => note.Id == id);
        noteData.noteList[index].Content += code;
    }
}
