using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionInfo = "Play Tape";
    [SerializeField] private Light projectorsLight;
    [SerializeField] private GameManager.GameStates changeStateOn;
    public bool Interactable = false;

    public void Interact()
    {
        if (Interactable)
        {
            projectorsLight.gameObject.SetActive(true);
            GameManager.Instance.UpdateGameState(changeStateOn);
            Interactable = false;
        }        
    }
    public string ShowActionInfo()
    {
        if (Interactable)
        {
            return actionInfo;
        }
        else return "";
    }
}
