using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostHearState : GhostStateMachineBase
{
    private NavMeshAgent ghostNavMeshAgent;
    private HearRange hearRange;
    private FieldOfView fieldOfView;

    private Animator ghostAnimator;
    private bool hasInicialized = false;

    private float ghostSpeed = 2f;
    private Vector3 soundDestination;
    private float hearDestinationDistance = 1.3f;
    public override void OnEnter(GhostStateMachine ghost)
    {
        Debug.Log("heared");
        if (hasInicialized == false)
        {
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            ghostAnimator = ghost.GetComponent<Animator>();
            hearRange = ghost.GetComponent<HearRange>();
            fieldOfView = ghost.GetComponent<FieldOfView>();
            hasInicialized = true;
        }
        ghostNavMeshAgent.speed = ghostSpeed;
        soundDestination = hearRange.lastHearedSoundPosition;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghostNavMeshAgent.destination = soundDestination;
        if (Vector3.Distance(ghost.transform.position, soundDestination) <= hearDestinationDistance)
        {           
            hearRange.soundHeard = false;
            ghost.ChangeState(ghost.SearchState);
        }
        else if (hearRange.lastHearedSoundPosition != soundDestination)
        {
            soundDestination = hearRange.lastHearedSoundPosition;
        }
        else if (fieldOfView.playerSeen)
        {
            ghost.ChangeState(ghost.ChaseState);
        }
    }
}
