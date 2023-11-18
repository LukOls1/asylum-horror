using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotePopUp : UIView
{
    [SerializeField] private TextMeshProUGUI popUpText;

    public static event Action<UIView> PopUpActive;
    public void ShowPopUp(Note note)
    {
        popUpText.text = note.Content;
        ActiveView(this);
        PopUpActive?.Invoke(this);
    }
}
