// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/EnemyStates/WanderingState")]
    public class WanderingState : EnemyState
    {
        private Transform _agentTransform;
        public WanderingState(EnemyNavAgent agent) : base(agent) {}

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            _agentTransform = _agent.gameObject.transform;
            PlayerDetection();
            
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }
        
        private void PlayerDetection()
        {
            Vector3 offset = new Vector3(0, .5f, 0);
            Physics.Raycast( _agentTransform.position + offset, _agentTransform.forward, _agent.Parameters.detectionRange);
        }
    }
    
}
