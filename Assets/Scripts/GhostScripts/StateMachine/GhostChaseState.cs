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
    private Transform playerTransform;
    private FirstPersonController playerController;

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
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            playerController = playerTransform.GetComponent<FirstPersonController>();
                       
            hasInicialized = true;
        }
        PlayGhostSound(AudioManager.Instance.ghostChaseSound);
        ghostNavMeshAgent.speed = ghostSpeed;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        destinationPosition = fieldOfView.LastPlayerSeenPosition;
        destinationDistance = Vector3.Distance(ghost.transform.position, fieldOfView.LastPlayerSeenPosition);
        
        // Chase player when he is on sight
        if (fieldOfView.PlayerSeen && !interactRange.PlayerCought)
        {
            ghostNavMeshAgent.destination = playerTransform.position;
        }
        // Kill player
        else if(fieldOfView.PlayerSeen && interactRange.PlayerCought)
        {
            ghostNavMeshAgent.destination = ghost.transform.position;
            ghost.ChangeState(ghost.GameOverState);
        }
        //Seen when hiding 
        else if (fieldOfView.playerSeenHiding)
        {
            fieldOfView.LastPlayerSeenPosition = playerController.lastPlayerPosition;
            if(destinationDistance <= sawDestinationDistance)
            {
                ghostNavMeshAgent.destination = ghost.transform.position;
                interactRange.killCamera.SetActive(true);
                ghost.ChangeState(ghost.GameOverState);
            }
            
        }
        // Going to last player position
        else if(!fieldOfView.PlayerSeen && destinationDistance > sawDestinationDistance && !interactRange.PlayerCought && !fieldOfView.playerSeenHiding)
        {
            ghostNavMeshAgent.destination = fieldOfView.LastPlayerSeenPosition;
        }
        // Ghost lost player sight. Goes to last sound position
        else if (destinationDistance <= sawDestinationDistance && hearRange.SoundHeard && !interactRange.PlayerCought && !fieldOfView.playerSeenHiding)
        {
            fieldOfView.LastPlayerSeenPosition = hearRange.LastHearedSoundPosition;
        }
        // Ghost lost all tracks
        else if(destinationDistance <= sawDestinationDistance && !hearRange.SoundHeard && !interactRange.PlayerCought && !fieldOfView.playerSeenHiding)
        {
            ghost.ChangeState(ghost.SearchState);
        } 
    }
    private void PlayGhostSound(AudioClip clip)
    {
        AudioManager.Instance.ghostSounds.Stop();
        AudioManager.Instance.PlayLoopSound(AudioManager.Instance.ghostSounds, clip);
    }
}
