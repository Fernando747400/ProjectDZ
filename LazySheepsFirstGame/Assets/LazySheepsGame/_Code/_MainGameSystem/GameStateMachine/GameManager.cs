using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using com.LazyGames.Dio;

public class GameManager : StateManager<GameStates>
{
    [Header("Dependencies")]

    [Required]
    public BoolEventChannelSO PauseEventChannel;

    [Required]
    public VoidEventChannelSO PauseInputChannel;



    #region Unity Methods

    private void Awake()
    {
        States.Add(GameStates.MainMenu, new GameMainMenuState(GameStates.MainMenu));
        States.Add(GameStates.Tabern, new GameTabernState(GameStates.Tabern));
        States.Add(GameStates.Playing, new GamePlayingState(GameStates.Playing));
        States.Add(GameStates.Paused, new GamePausedState(GameStates.Paused));
        States.Add(GameStates.Tabern, new GameWonState(GameStates.Won));
        States.Add(GameStates.Lost, new GameLostState(GameStates.Lost));

    }
    private void OnEnable()
    {
        PauseInputChannel.VoidEvent += PauseInputReceived;
    }

    private void Start()
    {
        CurrentState.EnterSate();
    }
    #endregion

    #region Private Methods

    private void PauseInputReceived()
    {

    }
    #endregion
}
