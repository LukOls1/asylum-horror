using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingItems : MonoBehaviour
{
    [SerializeField] private KeyCode leaveItemKey;
    [SerializeField] private Transform prefabSpawn;
    [SerializeField] private float pushForse = 5f;

    [SerializeField] private List<GameObject> inHandItems;
    [SerializeField] private List<GameObject> inHandItemPrefabs;
    public GameObject activeItem;

    private void Start()
    {
        Pickable.PickUpEvent += HandleItem;
    }
    private void Update()
    {
        if (Input.GetKeyDown(leaveItemKey) && activeItem != null)
        {
            LeaveItem();
        }
    }
    public void HandleItem(GameObject item)
    {
        if(activeItem != null)
        {
            SwitchItem(item);
        }
        else
        {
            TakeItem(item);
        }
    }
    public void LeaveItem()
    {
        GameObject item = inHandItemPrefabs.Find(obj => obj.name.Contains(activeItem.name));
        activeItem.SetActive(false);
        activeItem = null;
        Instantiate(item, prefabSpawn.position, transform.rotation);
        item.GetComponent<Rigidbody>().AddForce((transform.position - prefabSpawn.position).normalized * pushForse, ForceMode.Impulse);

    }
    private void SwitchItem(GameObject item)
    {
        LeaveItem();
        TakeItem(item);
    }
    private void TakeItem(GameObject item)
    {
        GameObject inHandItem = inHandItems.Find(obj => item.name.Contains(obj.name));
        activeItem = inHandItem;
        activeItem.SetActive(true);
    }
    public GameObject UseItem(GameObject item)
    {
        if(activeItem == null)
        {
            Debug.Log("You don't have any item");
            return null;
        }
        else if (!activeItem.name.Contains(item.name))
        {
            Debug.Log("The item doesn't match");
            return null;
        }
        else
        {
            activeItem.SetActive(false);
            activeItem = null;
            Debug.Log("item fits");
            return item;            
        }
    }
}
