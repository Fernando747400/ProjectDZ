using UnityEngine;
using UnityEngine.Events;

namespace Obvious.Soap
{
    [AddComponentMenu("Soap/EventListeners/EventListenerObjectives")]
    public class EventListenerObjectives : EventListenerGeneric<Objectives>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<Objectives>[] EventResponses => _eventResponses;

        [System.Serializable]
        public class EventResponse : EventResponse<Objectives>
        {
            [SerializeField] private ScriptableEventObjectives _scriptableEvent = null;
            public override ScriptableEvent<Objectives> ScriptableEvent => _scriptableEvent;

            [SerializeField] private ObjectivesUnityEvent _response = null;
            public override UnityEvent<Objectives> Response => _response;
        }

        [System.Serializable]
        public class ObjectivesUnityEvent : UnityEvent<Objectives>
        {
            
        }
    }
}