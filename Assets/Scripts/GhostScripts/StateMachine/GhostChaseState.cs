using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostChaseState : GhostStateMachineBase
{
    private NavMeshAgent ghostNavMeshAgent;
    private FieldOfView fieldOfView;
    private HearRange hearRange;

    private Animator ghostAnimator;
    private bool hasInicialized = false;

    private float ghostSpeed = 4f;
    private float sawDestinationDistance = 1.3f;
    public override void OnEnter(GhostStateMachine ghost)
    {
        Debug.Log("chase");
        if (hasInicialized == false)
        {
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            ghostAnimator = ghost.GetComponent<Animator>();
            hearRange = ghost.GetComponent<HearRange>();
            fieldOfView = ghost.GetComponent<FieldOfView>();
            hasInicialized = true;
        }
        ghostNavMeshAgent.speed = ghostSpeed;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
         
        if(fieldOfView.PlayerSeen)
        {
            ghostNavMeshAgent.destination = fieldOfView.Player.transform.position;
        }
        else if(!fieldOfView.PlayerSeen && Vector3.Distance(ghost.transform.position, fieldOfView.LastPlayerSeenPosition) > sawDestinationDistance)
        {
            ghostNavMeshAgent.destination = fieldOfView.LastPlayerSeenPosition;
        }
        else if(Vector3.Distance(ghost.transform.position, fieldOfView.LastPlayerSeenPosition) <= sawDestinationDistance && !hearRange.SoundHeard)
        {
            ghost.ChangeState(ghost.SearchState);
        }
        else if(Vector3.Distance(ghost.transform.position, fieldOfView.LastPlayerSeenPosition) <= sawDestinationDistance && hearRange.SoundHeard)
        {
            fieldOfView.LastPlayerSeenPosition = hearRange.LastHearedSoundPosition;
        }     
    }
}
