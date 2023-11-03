using System;
using UnityEngine;
using UnityEngine.Events;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "ScriptableObject/Events/GameState Event Channel")]
    public class GameStateEventChannelSO : MonoBehaviour
    {
        public UnityAction<Enum, GameObject> GameStateEvent;

        public void RaiseEvent(Enum state, GameObject sender)
        {
            GameStateEvent?.Invoke(state, sender);
        }
    }

}
