using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note
{
    public string Id;
    public string header;
    public string ContentPath;
    public string Content;
    public GameManager.GameStates ChangeStateOnThis;
}