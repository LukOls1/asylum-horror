using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionInfo = "TeddyBear";
    public static event Action<GameObject> PickUpEvent;

    public void Interact()
    {        
        Destroy(gameObject);
        PickUpEvent(gameObject);
    }
    public string ShowActionInfo()
    {
       return actionInfo;
    }
}
