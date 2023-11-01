//Dino 22/10/23
// This event channel is used to send an event to a listener with a HandHolder parameter.
using UnityEngine;
using UnityEngine.Events;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "ScriptableObject/Events/HandHolder Event Channel")]
    public class HandEventChannelSO : ScriptableObject
    {
        public UnityAction<HandHolder> HandHolderEvent;
        
        public void RaiseEvent(HandHolder value)
        {
            HandHolderEvent?.Invoke(value);
        }
        
    }
}