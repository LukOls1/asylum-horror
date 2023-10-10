using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateMachine : MonoBehaviour
{
    public GhostRoamState RoamState = new GhostRoamState();
    public GhostHearState HearState = new GhostHearState();
    public GhostSearchState SearchState = new GhostSearchState();
    public GhostChaseState ChaseState = new GhostChaseState();

    public GhostStateMachineBase currentState;

    private void Start()
    {
        currentState = RoamState;
        currentState.OnEnter(this);
    }
    private void Update()
    {
        currentState.OnUpdate(this);
    }

    public void ChangeState(GhostStateMachineBase state)
    {
        currentState = state;
        currentState.OnEnter(this);
    }
}
