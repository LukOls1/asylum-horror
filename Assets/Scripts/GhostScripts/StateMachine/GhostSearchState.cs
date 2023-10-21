using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostSearchState : GhostStateMachineBase
{
    private Animator ghostAnimator;
    private NavMeshAgent ghostNavMeshAgent;
    private HearRange hearRange;
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
            hasInicialized = true;
        }
        ghostAnimator.SetBool("idle", true);
        ghostAnimator.SetBool("walk", false);
        ghostAnimator.SetBool("fastWalk", false);

    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= animationLength && !hearRange.soundHeard)
        {
            elapsedTime = 0;
            ghost.ChangeState(ghost.RoamState);
        }
        else if(hearRange.soundHeard)
        {
            ghost.ChangeState(ghost.HearState);
        }
    }
    
}
