using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeBox : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionInfo;
    [SerializeField] private GameManager.GameStates ChangeStateOnThis;

    public void Interact()
    {
        Destroy(gameObject);
        GameManager.Instance.UpdateGameState(ChangeStateOnThis);
    }
    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
