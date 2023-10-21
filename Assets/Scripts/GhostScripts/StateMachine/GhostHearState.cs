using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostHearState : GhostStateMachineBase
{
    private NavMeshAgent ghostNavMeshAgent;
    private HearRange hearRange;

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
            hasInicialized = true;
        }
        ghostAnimator.SetBool("idle", false);
        ghostAnimator.SetBool("walk", false);
        ghostAnimator.SetBool("fastWalk", true);
        ghostNavMeshAgent.speed = ghostSpeed;
        soundDestination = hearRange.lastHearedSoundPosition;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghostNavMeshAgent.destination = soundDestination;
        Debug.Log(Vector3.Distance(ghost.transform.position, soundDestination).ToString());
        if (Vector3.Distance(ghost.transform.position, soundDestination) <= hearDestinationDistance)
        {           
            hearRange.soundHeard = false;
            ghost.ChangeState(ghost.SearchState);
        }
        else if (hearRange.lastHearedSoundPosition != soundDestination)
        {
            soundDestination = hearRange.lastHearedSoundPosition;
        }
    }
}
