using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingItems : MonoBehaviour
{
    [SerializeField] private KeyCode leaveItemKey;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform prefabSpawn;
    [SerializeField] private float pushForse = 3f;

    [SerializeField] private List<GameObject> inHandItems;
    [SerializeField] private List<GameObject> inHandItemPrefabs;
    public GameObject activeItem;
    private bool firstItemTaken = false;

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
        GameObject spawnedItem = Instantiate(item, prefabSpawn.position, transform.rotation);
        spawnedItem.GetComponent<Rigidbody>().AddForce((playerCamera.transform.forward).normalized * pushForse, ForceMode.Impulse);

    }
    private void SwitchItem(GameObject item)
    {
        LeaveItem();
        TakeItem(item);
    }
    private void TakeItem(GameObject item)
    {
        if (!firstItemTaken)
        {
            InformationManager.Instance.ShowTip(7);
            firstItemTaken = true;
        }
        GameObject inHandItem = inHandItems.Find(obj => item.name.Contains(obj.name));
        activeItem = inHandItem;
        activeItem.SetActive(true);
    }
    public GameObject UseItem()
    {
        return activeItem;
    }
}
