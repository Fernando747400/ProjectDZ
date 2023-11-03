using System;
using UnityEngine;

public class GamePausedState : BaseState<GameStates, GameManager>
{
    public GamePausedState(GameStates key, GameManager context) : base(key, context)
    {
    }

    public override void EnterState()
    {
        Context.PauseEventChannel.RaiseEvent(true);
        Context.PauseInputChannel.VoidEvent += PauseInput;
    }

    public override void ExitState()
    {
        Context.PauseEventChannel.RaiseEvent(false);
        Context.PauseInputChannel.VoidEvent -= PauseInput;
    }

    public override void FixedUpdateState()
    {
        throw new NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new NotImplementedException();
    }

    private void PauseInput()
    {
        Context.TransitionToState(Context.LastState.StateKey);
    }
}
