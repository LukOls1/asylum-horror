using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private FirstPersonController playerController;
    private bool screenLocked = false;
    private UIView currentViewActive;

    [SerializeField] private UIView pauseView;
    [SerializeField] private UIView journalView;
    [SerializeField] private UIView notePopUpView;

    [SerializeField] private KeyCode pause;
    [SerializeField] private KeyCode journal;
    [SerializeField] private KeyCode exit;

    private void Start()
    {
        NotePopUp.PopUpActive += HandleView;
    }
    void Update()
    {
        if (Input.GetKeyDown(exit) && currentViewActive != null)
        {
            currentViewActive.ControlView(currentViewActive);
            currentViewActive = null;
            LockScreen();
        }
        else if (Input.GetKeyDown(pause))
        {
            HandleView(pauseView);
        }
        if (Input.GetKeyDown(journal))
        {
            HandleView(journalView);
        }
    }
    private void HandleView(UIView view)
    {
        LockScreen();
        SetThisCurrent(view);
        view.ControlView(currentViewActive);
    }
    public void SetThisCurrent(UIView controlledView)
    {
        if (currentViewActive != controlledView && currentViewActive != null)
        {
            return;
        }
        else if( currentViewActive == null)
        {
            currentViewActive = controlledView;
        }
        else currentViewActive = null;  
    }
    private void LockScreen()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            screenLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            screenLocked = true;
        }
        playerController.enabled = !playerController.enabled;
    }
}
