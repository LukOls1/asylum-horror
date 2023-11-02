using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private FirstPersonController playerMainScript;
    [SerializeField] private HearRange hearRange;
    private Vector3 originalJointPosition;

    [SerializeField] private float stepTrigger = 0.01f;
    
    private bool stepDone = true;

    private void Start()
    {
        originalJointPosition = playerMainScript.joint.localPosition;
    }

    private void Update()
    {
        AplyFootstepsSound();
    }
    private void AplyFootstepsSound()
    {
        if (playerMainScript.joint.localPosition.y <= (originalJointPosition.y - playerMainScript.bobAmount.y + stepTrigger) && !playerMainScript.IsCrouched && stepDone == false)
        {
            MakeSound();
            stepDone = true;
        }
        else if (playerMainScript.joint.localPosition.y > originalJointPosition.y && stepDone == true)
        {
            stepDone = false;
        }
    }
    public void MakeSound()
    {
        stepSound.Play();
        if(hearRange.isActiveAndEnabled)
        {
            transform.GetComponent<EventSoundMaker>().SendSoundMakerTransform(transform);
        }
    }
}
