using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostGameOverState : GhostStateMachineBase
{
    private Animator ghostAnimator;
    private NavMeshAgent ghostNavMeshAgent;

    private bool hasInicialized = false;

    private float ghostSpeed = 0f;

    public override void OnEnter(GhostStateMachine ghost)
    {
        if(hasInicialized == false)
        {
            ghostAnimator = ghost.GetComponent<Animator>();
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            hasInicialized = true;
        }
        ghostNavMeshAgent.speed = ghostSpeed;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghostAnimator.SetBool("kill", true);
    }
}
