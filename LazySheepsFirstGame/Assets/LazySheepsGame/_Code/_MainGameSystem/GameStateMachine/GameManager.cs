using UnityEngine;
using NaughtyAttributes;
using com.LazyGames.Dio;

public class GameManager : StateManager<GameStates, GameManager>
{
    [Header("Dependencies")]

    [Required]
    [SerializeField] internal BoolEventChannelSO PauseEventChannel;

    //[Required]
    [SerializeField] internal VoidEventChannelSO PauseInputChannel;


    #region Unity Methods

    private void Awake()
    {
        States.Add(GameStates.MainMenu, new GameMainMenuState(GameStates.MainMenu, this));
        States.Add(GameStates.Tabern, new GameTabernState(GameStates.Tabern, this));
        States.Add(GameStates.Playing, new GamePlayingState(GameStates.Playing, this));
        States.Add(GameStates.Paused, new GamePausedState(GameStates.Paused, this));
        States.Add(GameStates.Tabern, new GameWonState(GameStates.Won, this));
        States.Add(GameStates.Lost, new GameLostState(GameStates.Lost, this));

        CurrentState = States[GameStates.Tabern];
    }
    private void OnEnable()
    {
        PauseInputChannel.VoidEvent += PauseInputReceived;
    }
    #endregion

    #region Private Methods

    private void PauseInputReceived()
    {
        TransitionToState(GameStates.Paused);
    }
    #endregion
}
