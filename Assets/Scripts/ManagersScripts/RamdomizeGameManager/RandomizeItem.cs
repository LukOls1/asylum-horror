using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeItem : MonoBehaviour
{
    private List<Transform> randomSpawnPoints;
    public List<Transform> RandomItemSpawnPoints
    {
        get         { return randomSpawnPoints; }
        private set { randomSpawnPoints = value; }
    }
    private void Awake()
    {
        randomSpawnPoints = new List<Transform>();
    }
    public List<Transform> GenerateSpawnPoints(int itemsQuantity, List<Transform> spawnPointList)
    {
        int randomIndex;
        for (int i = 0; i < itemsQuantity; i++)
        {
            randomIndex = Random.Range(0, spawnPointList.Count);
            randomSpawnPoints.Add(spawnPointList[randomIndex]);
            spawnPointList.RemoveAt(randomIndex);
        }
        return RandomItemSpawnPoints;
    }
}
