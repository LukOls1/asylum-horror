using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostHearState : GhostStateMachineBase
{
    private NavMeshAgent ghostNavMeshAgent;
    private HearRange hearRange;
    private FieldOfView fieldOfView;

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
            fieldOfView = ghost.GetComponent<FieldOfView>();
            hasInicialized = true;
        }
        PlayGhostSound(AudioManager.Instance.ghostAlertedSound);
        ghostNavMeshAgent.speed = ghostSpeed;
        soundDestination = hearRange.LastHearedSoundPosition;
    }
    public override void OnUpdate(GhostStateMachine ghost)
    {
        ghostNavMeshAgent.destination = soundDestination;
        if (Vector3.Distance(ghost.transform.position, soundDestination) <= hearDestinationDistance)
        {           
            hearRange.SoundHeard = false;
            ghost.ChangeState(ghost.SearchState);
        }
        else if (hearRange.LastHearedSoundPosition != soundDestination)
        {
            soundDestination = hearRange.LastHearedSoundPosition;
        }
        else if (fieldOfView.PlayerSeen)
        {
            ghost.ChangeState(ghost.ChaseState);
        }
    }
    private void PlayGhostSound(AudioClip clip)
    {
        AudioManager.Instance.ghostSounds.Stop();
        AudioManager.Instance.PlayLoopSound(AudioManager.Instance.ghostSounds, clip);
    }
}
