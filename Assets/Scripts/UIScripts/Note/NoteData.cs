using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Create Notes Data", fileName = "Notes Data")]
public class NoteData : ScriptableObject
{
    [SerializeField] public List<Note> noteList;
    public void ConvertFileToTxt()
    {
        foreach (Note note in noteList)
        {
            if (File.Exists(note.ContentPath))
            {
                note.Content = File.ReadAllText(note.ContentPath);
            }
            else
            {
                Debug.LogError("There is no note file of: " + note.ContentPath);
            }
        }
    }

}
