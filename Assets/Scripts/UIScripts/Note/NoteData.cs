using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Create Notes Data", fileName = "Notes Data")]
public class NoteData : ScriptableObject
{
    [SerializeField] public List<Note> noteList;
}
