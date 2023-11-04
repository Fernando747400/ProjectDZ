using System.Collections.Generic;
using System;
using UnityEngine;
using com.LazyGames.DZ;
using NaughtyAttributes;

public abstract class StateManager<BState, TContext> : MonoBehaviour where BState : Enum
{

    [Header("Base Dependencies")]

    [Required]
    [SerializeField] private GameStateEventChannelSO _stateChangedEventChannel;

    public BaseState<BState, TContext> CurrentState;
    protected Dictionary<BState, BaseState<BState, TContext>> States = new Dictionary<BState, BaseState<BState, TContext>>();
    internal BaseState<BState, TContext> LastState { get; private set; }

    private BState _queuedState;
    protected TContext Context;

    protected void Start()
    {
        EnterState(CurrentState);
    }

    protected void Update()
    {
        if (ShouldTransition())
        {
            ExecuteTransition(_queuedState);
        }
        else
        {
            UpdateState(CurrentState);
        }
    }

    protected void FixedUpdate()
    {
        FixedUpdateState(CurrentState);
    }

    public void TransitionToState(BState stateKey)
    {
        _queuedState = stateKey;
    }

    protected virtual void EnterState(BaseState<BState, TContext> state)
    {
        state.EnterState();
    }

    protected virtual void UpdateState(BaseState<BState, TContext> state)
    {
        state.UpdateState();
    }

    protected virtual void FixedUpdateState(BaseState<BState, TContext> state)
    {
        state.FixedUpdateState();
    }

    protected virtual bool ShouldTransition()
    {
        return !_queuedState.Equals(CurrentState.StateKey);
    }

    protected virtual void ExecuteTransition(BState stateKey)
    {
        LastState = CurrentState;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        _stateChangedEventChannel.RaiseEvent(CurrentState.StateKey, this.gameObject);
        EnterState(CurrentState);
    }
}
