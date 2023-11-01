using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostChaseState : GhostStateMachineBase
{
    private NavMeshAgent ghostNavMeshAgent;
    private FieldOfView fieldOfView;
    private HearRange hearRange;
    private InteractRange interactRange;

    private bool hasInicialized = false;

    private float ghostSpeed = 4f;

    private float destinationDistance;
    private Vector3 destinationPosition;
    private float sawDestinationDistance = 1.3f;
    public override void OnEnter(GhostStateMachine ghost)
    {
        Debug.Log("chase");
        if (hasInicialized == false)
        {
            ghostNavMeshAgent = ghost.GetComponent<NavMeshAgent>();
            fieldOfView = ghost.GetComponent<FieldOfView>();
            hearRange = ghost.GetComponent<HearRange>();
            interactRange = ghost.GetComponent<InteractRange>();
                       
            hasInicialized = true;
        }
        ghostNavMeshAgent.speed = ghostSpeed;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        destinationPosition = fieldOfView.LastPlayerSeenPosition;
        destinationDistance = Vector3.Distance(ghost.transform.position, fieldOfView.LastPlayerSeenPosition);
        
        // Chase player when he is on sight
        if (fieldOfView.PlayerSeen && !interactRange.PlayerCought)
        {
            Debug.Log("Chase player when he is on sight");
            ghostNavMeshAgent.destination = destinationPosition;
        }
        // Kill player
        else if(fieldOfView.PlayerSeen && interactRange.PlayerCought)
        {
            Debug.Log("Kill");
            ghostNavMeshAgent.destination = ghost.transform.position;
            ghost.ChangeState(ghost.GameOverState);
        }
        // Going to last player position
        else if(!fieldOfView.PlayerSeen && destinationDistance > sawDestinationDistance && !interactRange.PlayerCought)
        {
            Debug.Log("Going to last player position");
            ghostNavMeshAgent.destination = fieldOfView.LastPlayerSeenPosition;
        }
        // Ghost lost player sight. Goes to last sound position
        else if (destinationDistance <= sawDestinationDistance && hearRange.SoundHeard && !interactRange.PlayerCought)
        {
            Debug.Log("Ghost lost player sight. Goes to last sound position");
            fieldOfView.LastPlayerSeenPosition = hearRange.LastHearedSoundPosition;
        }
        // Ghost lost all tracks
        else if(destinationDistance <= sawDestinationDistance && !hearRange.SoundHeard && !interactRange.PlayerCought)
        {
            Debug.Log("Ghost lost player sight. Goes to last sound position");
            ghost.ChangeState(ghost.SearchState);
        } 
    }
}
