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

    private float ghostSpeed = 3f;
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
        ghostAnimator.SetBool("idle", false);
        ghostAnimator.SetBool("walk", false);
        ghostAnimator.SetBool("fastWalk", false);
        ghostAnimator.SetBool("run", true);
        ghostNavMeshAgent.speed = ghostSpeed;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
         
        if(fieldOfView.playerSeen)
        {
            ghostNavMeshAgent.destination = fieldOfView.player.transform.position;
        }
        else if(!fieldOfView.playerSeen && Vector3.Distance(ghost.transform.position, fieldOfView.lastPlayerSeenPosition) > sawDestinationDistance)
        {
            ghostNavMeshAgent.destination = fieldOfView.lastPlayerSeenPosition;
        }
        else if(Vector3.Distance(ghost.transform.position, fieldOfView.lastPlayerSeenPosition) <= sawDestinationDistance && !hearRange.soundHeard)
        {
            ghost.ChangeState(ghost.SearchState);
        }
        else if(Vector3.Distance(ghost.transform.position, fieldOfView.lastPlayerSeenPosition) <= sawDestinationDistance && hearRange.soundHeard)
        {
            fieldOfView.lastPlayerSeenPosition = hearRange.lastHearedSoundPosition;
        }     
    }
}
