using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionInfo = "Exit";

    public void Interact()
    {
        if(GameManager.Instance.ObjectivesDone == false)
        {
            InformationManager.Instance.ShowTip(12);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameStates.GameOver);
        }
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
