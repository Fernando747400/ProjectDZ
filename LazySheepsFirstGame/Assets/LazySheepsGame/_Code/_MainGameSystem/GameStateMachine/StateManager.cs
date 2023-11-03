using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateManager<BState> : MonoBehaviour where BState : Enum
{
    protected Dictionary<BState, BaseState<BState>> States = new Dictionary<BState, BaseState<BState>>();
    protected BaseState<BState> CurrentState;
    protected BaseState<BState> LastState;

    private bool _inTransition;

    #region Unity Methods

    private void Start()
    {
        CurrentState.EnterSate();
    }

    private void Update()
    {
        BState nextStateKey = CurrentState.GetNextState();

        if (!_inTransition && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();
        }
        else
        {
            TransitionToState(nextStateKey);
        }
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdateState();
    }
    #endregion

    #region Public Methods
    public void TransitionToState(BState stateKey)
    {
        _inTransition = true;
        CurrentState.ExitState();
        LastState = CurrentState;
        CurrentState = States[stateKey];
        CurrentState.EnterSate();
        _inTransition = false;
    }
    #endregion
}
