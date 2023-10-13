using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource stepSound;
    [SerializeField] FirstPersonController playerMainScript;
    [SerializeField] private float stepTrigger = 0.01f;
    private Vector3 originalJointPosition;
    private bool stepDone = false;

    private void Start()
    {
        originalJointPosition = playerMainScript.joint.localPosition;
    }

    private void Update()
    {
        Debug.Log(playerMainScript.joint.localPosition);
        if (playerMainScript.joint.localPosition.y <= (originalJointPosition.y - playerMainScript.bobAmount.y + stepTrigger) && stepDone == false)
        {
            PlayStepSound();
            stepDone = true;
        }
        else if (playerMainScript.joint.localPosition == originalJointPosition && stepDone == true)
        {
            stepDone = false;
        }
    }
    private void PlayStepSound()
    {
        stepSound.Play();
    }
}
