using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemsList;
    [SerializeField] private GameObject spawnItemPointsContainer;
    [SerializeField] private RandomizeItem randomizeItem;
    private List<Transform> spawnItemPointList;
    public List<Transform> SpawnItemPointList
    {
        get         { return spawnItemPointList; }
        private set { spawnItemPointList = value; }
    }

    private void Awake()
    {
        spawnItemPointList = new List<Transform>();
        GetSpawnPointsList();
        RandomItemsSpawn();
    }

    private List<Transform> GetSpawnPointsList()
    {
        foreach (Transform point in spawnItemPointsContainer.transform)
        {
            spawnItemPointList.Add(point);
        }
        return SpawnItemPointList;
    }

    private void RandomItemsSpawn()
    {
        List<Transform> randomList = randomizeItem.GenerateSpawnPoints(itemsList.Count, spawnItemPointList);
        for (int i = 0; i < randomList.Count; i++)
        {
            Instantiate(itemsList[i], randomList[i].position, itemsList[i].transform.rotation);
        }
    }
}
