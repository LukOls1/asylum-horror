using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostSearchState : GhostStateMachineBase
{
    private Animator ghostAnimator;
    private NavMeshAgent ghostNavMeshAgent;
    private HearRange hearRange;
    private FieldOfView fieldOfView;
    private bool hasInicialized = false;
    private float elapsedTime;
    private int animationLength = 4;
    public override void OnEnter(GhostStateMachine ghost)
    {
        Debug.Log("search");
        if (hasInicialized == false)
        {
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            ghostAnimator = ghost.GetComponent<Animator>();
            hearRange = ghost.GetComponent<HearRange>();
            fieldOfView = ghost.GetComponent<FieldOfView>();
            hasInicialized = true;
        }
        ghostNavMeshAgent.speed = 0;
        ghostAnimator.SetBool("idle", true);

    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= animationLength && !hearRange.soundHeard && !fieldOfView.playerSeen)
        {
            elapsedTime = 0;
            ghostAnimator.SetBool("idle", false);
            ghost.ChangeState(ghost.RoamState);
        }
        else if (hearRange.soundHeard && !fieldOfView.playerSeen)
        {
            ghost.ChangeState(ghost.HearState);
        }
        else if (fieldOfView.playerSeen)
        {
            ghost.ChangeState(ghost.ChaseState);
        }
    }
    
}
