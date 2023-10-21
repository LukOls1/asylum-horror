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
        AplyFootstepsSound();
    }
    private void AplyFootstepsSound()
    {
        if (playerMainScript.joint.localPosition.y <= (originalJointPosition.y - playerMainScript.bobAmount.y + stepTrigger) && !playerMainScript.isCrouched && stepDone == false)
        {
            MakeSound();
            stepDone = true;
        }
        else if (playerMainScript.joint.localPosition.y > originalJointPosition.y && stepDone == true)
        {
            SoundOver();
            stepDone = false;
        }
    }
    public void MakeSound()
    {
        stepSound.Play();
        transform.GetComponent<EventSoundMaker>().SendSoundMakerTransform(transform);
    }
    public void SoundOver()
    {

    }

}
