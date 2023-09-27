// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/EnemyStates/IdleState")]

    public class IdleState : EnemyState
    {
        
        public IdleState(EnemyNavAgent agent) : base(agent) {}
        
        public override void EnterState()
        {
            _agent.DoChase = false;
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }
    }
}
