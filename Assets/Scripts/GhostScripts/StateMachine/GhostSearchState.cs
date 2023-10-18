using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostSearchState : GhostStateMachineBase
{
    private Animator ghostAnimator;
    private NavMeshAgent ghostNavMeshAgent;
    private bool hasInicialized = false;
    private float elapsedTime;
    private int animationLength = 4;
    public override void OnEnter(GhostStateMachine ghost)
    {
        if(hasInicialized == false)
        {
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            ghostAnimator = ghost.GetComponent<Animator>();
            hasInicialized = true;
        }
        ghostAnimator.SetBool("idle", true);

    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= animationLength)
        {
            elapsedTime = 0;
            ghost.ChangeState(ghost.RoamState);
        }
    }
    
}
