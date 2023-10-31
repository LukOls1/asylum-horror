using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateMachine : MonoBehaviour
{
    public GhostRoamState RoamState = new GhostRoamState();
    public GhostHearState HearState = new GhostHearState();
    public GhostSearchState SearchState = new GhostSearchState();
    public GhostChaseState ChaseState = new GhostChaseState();

    public GhostStateMachineBase CurrentState;

    private void Start()
    {
        CurrentState = SearchState;
        CurrentState.OnEnter(this);
    }
    private void Update()
    {
        CurrentState.OnUpdate(this);
    }

    public void ChangeState(GhostStateMachineBase state)
    {
        CurrentState = state;
        CurrentState.OnEnter(this);
    }
}
