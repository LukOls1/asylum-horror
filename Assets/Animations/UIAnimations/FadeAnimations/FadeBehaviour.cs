using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBehaviour : StateMachineBehaviour
{
    public GameManager.GameStates changeStateOn;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("fadeActive", false);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(changeStateOn != GameManager.GameStates.IdleState)
        {
            GameManager.Instance.UpdateGameState(changeStateOn);
        }
    }
}
