using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSpawnPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    
    public Transform ReturnRandomPoint()
    {
        int count = spawnPoints.Count;
        return spawnPoints[Random.Range(0, count)];
    }
}
