using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostRoamState : GhostStateMachineBase
{
    private Transform destinationRoamPoint;
    private NavMeshAgent ghostNavMeshAgent;
    private List<Transform> roamPoints;
    private Animator ghostAnimator;
    private int randomRoampointIndex = -1;
    private float roamDestinationDistance = 1f;
    private bool hasInicialized = false;
    public override void OnEnter(GhostStateMachine ghost)
    {
        if(hasInicialized == false)
        {            
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            ghostAnimator = ghost.GetComponent<Animator>();
            InicializePointsList();
            hasInicialized = true;
        }
        destinationRoamPoint = ReturnRandomRoamPoint();
        ghostAnimator.SetBool("idle", false);
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghostNavMeshAgent.destination = destinationRoamPoint.position;
        if(Vector3.Distance(ghost.transform.position, destinationRoamPoint.position) <= roamDestinationDistance)
        {
            ghost.ChangeState(ghost.SearchState);
        }
    }
    private void InicializePointsList()
    {
        roamPoints = new List<Transform>();
        GameObject[] roamGameObjects = GameObject.FindGameObjectsWithTag("RoamPoint");
        foreach (GameObject point in roamGameObjects)
        {
            roamPoints.Add(point.transform);
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
