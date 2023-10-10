using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostStateMachineBase
{
    public abstract void OnEnter(GhostStateMachine ghost);
    public abstract void OnUpdate(GhostStateMachine ghost);

}
