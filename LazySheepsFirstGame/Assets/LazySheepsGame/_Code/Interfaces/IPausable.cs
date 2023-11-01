using com.LazyGames.Dio;

namespace com.LazyGames
{
    public interface IPausable
    {
        public BoolEventChannelSO PauseUpdateChannel {get; set;}

        public bool IsPaused {get; set;}

        void SubscribePause()
        {
            PauseUpdateChannel.BoolEvent += UpdatePause;
        }

        void UnsubscribePause()
        {
            PauseUpdateChannel.BoolEvent -= UpdatePause;
        }

        void UpdatePause(bool newValue)
        {
            IsPaused = newValue;
        }
    }
}

