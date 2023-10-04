// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    public class IdleState : EnemyState
    {
        public override void EnterState()
        {
            Agent.DoChase = false;
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
