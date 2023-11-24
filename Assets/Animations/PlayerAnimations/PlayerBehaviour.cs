using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBehaviour
{
    private FirstPersonController fpc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fpc = animator.transform.GetComponent<FirstPersonController>();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fpc = animator.transform.GetComponent<FirstPersonController>();
        fpc.CameraCanMove = true;
        fpc.PlayerCanMove = true;
        animator.applyRootMotion = true;
        //tym trzeba nadac roota animator.rootPosition
        //animator.enabled = false;
    }
}
