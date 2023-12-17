using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostResetState : GhostStateMachineBase
{
    private NavMeshAgent ghostNavMeshAgent;
    private FieldOfView fieldOfView;
    private HearRange hearRange;
    private InteractRange interactRange;

    private bool hasInicialized = false;

    public override void OnEnter(GhostStateMachine ghost)
    {
        if (hasInicialized == false)
        {
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            fieldOfView = ghost.GetComponent<FieldOfView>();
            hearRange = ghost.GetComponent<HearRange>();
            interactRange = ghost.GetComponent<InteractRange>();

            hasInicialized = true;
        }
        fieldOfView.PlayerSeen = false;
        fieldOfView.playerSeenHiding = false;
        hearRange.SoundHeard = false;
        interactRange.PlayerCought = false;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghost.ChangeState(ghost.SearchState);
    }
}
