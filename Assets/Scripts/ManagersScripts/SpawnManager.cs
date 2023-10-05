using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemsList;
    [SerializeField] private List<Transform> spawnPointList;

    private void Start()
    {
        RandomItemsSpawn();
    }

    private void RandomItemsSpawn()
    {
        List<Transform> points = spawnPointList;
        int listCount = points.Count;
        int randomIndex;
        foreach (GameObject item in itemsList)
        {
            randomIndex = Random.Range(0, listCount);
            Instantiate(item, points[randomIndex].position, item.transform.rotation);
            points.RemoveAt(randomIndex);
            listCount--;
        }
    }
}
