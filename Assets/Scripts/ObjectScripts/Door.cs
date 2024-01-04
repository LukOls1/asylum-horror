using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private string actionInfo;
    private bool isClosed = true;
    public bool IsLocked = false;

    [SerializeField] private GameManager.GameStates unlockOnState;

    public DoorState doorState;
    public enum DoorState 
    { 
        Opened,
        Closed,
        Locked
    }

    private void Awake()
    {
        doorState = DoorState.Locked;
        unlockOnState = GameManager.GameStates.Part1;
        GameManager.OnGameStateChange += EventFlowControll;
        animator = gameObject.GetComponent<Animator>();
    }
    public void Interact()
    {
        if(doorState != DoorState.Locked)
        {
            animator.SetTrigger("use");
            isClosed = !isClosed;
            animator.SetBool("isClosed", isClosed);
            if (doorState == DoorState.Closed)
            {
                doorState = DoorState.Opened;
            }
            else doorState = DoorState.Closed;
        }
    }
    public string ShowActionInfo()
    {
        switch (doorState)
        {
            case DoorState.Opened:
                actionInfo = "Close";
                break;
            case DoorState.Closed:
                actionInfo = "Open";
                break;
            case DoorState.Locked:
                actionInfo = "Locked";
                break;
            default:
                break;
        }
        return actionInfo;
    }
    public void EventFlowControll(GameManager.GameStates state)
    {
        if(doorState == DoorState.Locked)
        {
            if (state == unlockOnState && !IsLocked)
            {
                doorState = DoorState.Closed;
            }
        }
    }
}
