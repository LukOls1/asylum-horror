using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private FadeBehaviour fadeBehaviour;
    private Animator fadeAnimator;

    private void Start()
    {
        fadeAnimator = GetComponent<Animator>();
    }
    public void FadeAndChangeState(GameManager.GameStates state)
    {
        fadeBehaviour.changeStateOn = state;
        fadeAnimator.SetBool("fadeActive", true);
    }   
}
