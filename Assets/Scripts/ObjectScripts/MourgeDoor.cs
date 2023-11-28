using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MourgeDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionInfo = "Place Item";
    [SerializeField] private PickingItems pickingItems;
    [SerializeField] private List<GameObject> neededItems;
    
    private int currentItemIndex = 0;

    private void Start()
    {
        if(neededItems.Count <= 0)
        {
            throw new ArgumentException("The list of items is empty", nameof(neededItems));
        }
    }
    public void Interact()
    {       
        GameObject itemHolder = pickingItems.UseItem(neededItems[currentItemIndex]);
        if(itemHolder == null)
        {
            return;
        }
        if (itemHolder.name.Contains(neededItems[currentItemIndex].name) && neededItems.Count > currentItemIndex)
        {
            currentItemIndex++;
        }
        if (neededItems.Count == currentItemIndex)
        {
            Debug.Log("Body burned");
        }
    }

    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
