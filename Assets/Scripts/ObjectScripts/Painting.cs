using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour, IInteractable
{
    private string actionInfo = "Painting";
    [SerializeField] private Animation paintingAnimation;
    public bool Interactable = false;

    public void Interact()
    {
        if (Interactable)
        {
            paintingAnimation.Play();
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
