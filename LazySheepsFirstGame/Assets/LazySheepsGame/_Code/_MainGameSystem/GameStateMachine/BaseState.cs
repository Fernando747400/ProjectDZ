using System;
using UnityEngine;

public abstract class BaseState<BState, TContext> where BState : Enum
{
    public BaseState(BState key, TContext context)
    {
        StateKey = key;
        Context = context;
    }

    public BState StateKey { get; private set; }
    protected TContext Context { get; private set; }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}
