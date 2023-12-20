using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionInfo = "Exit";

    private void Start()
    {
        
    }
    public void Interact()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameStates.GameOver);
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
