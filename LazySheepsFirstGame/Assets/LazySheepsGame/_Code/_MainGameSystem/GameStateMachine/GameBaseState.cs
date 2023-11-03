using System;
using UnityEngine;

public abstract class GameBaseState<BState> where BState : Enum
{
    public GameBaseState(BState key)
    {
        StateKey = key;
    }

    public BState StateKey { get; private set; }

    public abstract void EnterSate();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract BState GetNextState();
}
