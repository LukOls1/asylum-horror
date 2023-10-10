using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostRoamState : GhostStateMachineBase
{
    private Transform destinationRoamPoint;
    public override void OnEnter(GhostStateMachine ghost)
    {
        destinationRoamPoint = ghost.GetComponent<RoamPointsController>().ReturnRandomRoamPoint();
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghost.GetComponent<NavMeshAgent>().destination = destinationRoamPoint.position;
    }
}
