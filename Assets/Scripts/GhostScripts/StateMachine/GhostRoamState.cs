using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostRoamState : GhostStateMachineBase
{
    private NavMeshAgent ghostNavMeshAgent;
    private HearRange hearRange;
    private FieldOfView fieldOfView;

    private Transform destinationRoamPoint;
    private List<Transform> roamPoints;
    private Animator ghostAnimator;
    private int randomRoampointIndex = -1;
    private float roamDestinationDistance = 1f;
    private bool hasInicialized = false;

    private float ghostSpeed = 1.2f;
    public override void OnEnter(GhostStateMachine ghost)
    {
        Debug.Log("roam");
        if (hasInicialized == false)
        {            
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            ghostAnimator = ghost.GetComponent<Animator>();
            hearRange = ghost.GetComponent<HearRange>();
            fieldOfView = ghost.GetComponent<FieldOfView>();
            InicializePointsList();
            hasInicialized = true;
        }
        ghostNavMeshAgent.speed = ghostSpeed;
        destinationRoamPoint = ReturnRandomRoamPoint();
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghostNavMeshAgent.destination = destinationRoamPoint.position;
        if (Vector3.Distance(ghost.transform.position, destinationRoamPoint.position) <= roamDestinationDistance && !hearRange.soundHeard)
        {
            ghost.ChangeState(ghost.SearchState);
        }
        else if(hearRange.soundHeard && !fieldOfView.playerSeen)
        {
            ghost.ChangeState(ghost.HearState);
        }
        else if (fieldOfView.playerSeen)
        {
            ghost.ChangeState(ghost.ChaseState);
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
