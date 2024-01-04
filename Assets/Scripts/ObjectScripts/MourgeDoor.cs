using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MourgeDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string actionInfo = "Place Item";
    [SerializeField] private PickingItems pickingItems;
    [SerializeField] private List<GameObject> neededItems;
    
    private ParticleSystem smokeParticle;
    private Animator doorAnimator;

    private int currentItemIndex = 0;

    public static event Action BodyBurned;

    private void Start()
    {
        smokeParticle = transform.GetComponentInChildren<ParticleSystem>();
        doorAnimator = transform.GetComponent<Animator>();
    }
    public void Interact()
    {
        GameObject itemHolder = pickingItems.UseItem();
        if(itemHolder == null)
        {
            InformationManager.Instance.ShowTip(8);
        }
        else if (neededItems.Count > 0)
        {          
            if (itemHolder.name.Contains(neededItems[currentItemIndex].name) && neededItems.Count > currentItemIndex)
            {
                pickingItems.activeItem.SetActive(false);
                pickingItems.activeItem = null;
                doorAnimator.SetTrigger("open");
                InformationManager.Instance.ShowTip(11);
                currentItemIndex++;
            }
            else InformationManager.Instance.ShowTip(9);
            if (neededItems.Count == currentItemIndex)
            {
                BodyBurned?.Invoke();
                InformationManager.Instance.ShowTip(10);
                smokeParticle.Play();
                actionInfo = "Burned Cell";
            }
        }
        else InformationManager.Instance.ShowTip(9);
    }


    public string ShowActionInfo()
    {
        return actionInfo;
    }
}
