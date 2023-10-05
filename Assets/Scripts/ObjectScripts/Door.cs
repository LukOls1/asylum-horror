using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Animator animator;
    private string actionInfoPositive = "Open";
    private string actionInfoNegative = "Close";
    private bool isClosed = true;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void Interact()
    {
        animator.SetTrigger("use");
        isClosed = !isClosed;
        animator.SetBool("isClosed", isClosed);
    }
    public string ShowActionInfo()
    {
        return !isClosed ? actionInfoNegative : actionInfoPositive;
    }
}
