using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamPointsController : MonoBehaviour
{
    [SerializeField] private GameObject navMeshRoamPoints;
    private List<Transform> roamPoints;
    private int randomRoampointIndex = -1;

    private void Awake()
    {
        roamPoints = new List<Transform>();
        InicializePointsList();
    }

    private void InicializePointsList()
    {
        foreach (Transform point in navMeshRoamPoints.transform)
        {
            roamPoints.Add(point);
        }
    }
    
    public Transform ReturnRandomRoamPoint()
    {
        int indexPoint;
        do
        {
            indexPoint = Random.Range(0, roamPoints.Count);
        } while (indexPoint == randomRoampointIndex);
        randomRoampointIndex = indexPoint;
        return roamPoints[randomRoampointIndex];
    }
}
