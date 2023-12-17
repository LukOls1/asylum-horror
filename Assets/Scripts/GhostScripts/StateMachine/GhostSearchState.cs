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
    private float ghostSpeed = 0f;
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
        ghostNavMeshAgent.speed = ghostSpeed;
        ghostAnimator.SetBool("idle", true);
        ghostAnimator.SetBool("kill", false);

    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= animationLength && !hearRange.SoundHeard && !fieldOfView.PlayerSeen)
        {
            elapsedTime = 0;
            ghostAnimator.SetBool("idle", false);
            ghost.ChangeState(ghost.RoamState);
        }
        else if (hearRange.SoundHeard && !fieldOfView.PlayerSeen)
        {
            ghost.ChangeState(ghost.HearState);
        }
        else if (fieldOfView.PlayerSeen)
        {
            ghost.ChangeState(ghost.ChaseState);
        }
    }
    
}
