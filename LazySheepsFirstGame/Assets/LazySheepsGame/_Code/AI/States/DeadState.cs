// Creado Raymundo Mosqueda 04/10/23

using UnityEngine;
using System.Collections;
using Lean.Pool;

namespace com.LazyGames.DZ
{
    public class DeadState : EnemyState
    {
        public override void EnterState()
        {
            Controller.agent.speed = 0;
            // Controller.col
        }
        
        public override void UpdateState()
        {
            // listen for respawn event
            // set animator to dead pose
        }
        
        public override void ExitState()
        {
            Controller.hP = Controller.parameters.maxHp;
            Controller.agent.speed = Controller.parameters.baseSpeed;
        }
        
        private void Despawn()
        {
            LeanPool.Despawn(this.gameObject);
        }
    }
}
