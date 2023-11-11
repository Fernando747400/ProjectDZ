// Creado Raymundo Mosqueda 04/10/23

using UnityEngine;
using System.Collections;
using Lean.Pool;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;

namespace com.LazyGames.DZ
{
    public class DeadState : EnemyState
    {
        public override void EnterState()
        {
            Controller.agent.speed = 0;
            Controller.doHear = false;
        }
        
        public override void UpdateState()
        {
            Controller.agent.isStopped = true;
            // listen for respawn event
            // set animator to dead pose
        }
        
        public override void ExitState()
        {
            Controller.agent.isStopped = false;
            Controller.hP = Controller.parameters.maxHp;
            Controller.agent.speed = Controller.parameters.baseSpeed;
            Controller.doHear = true;
        }

        public override void SetAnimation()
        {
            var newAnimState = "Dead";
            if (newAnimState == Controller.currentAnimState) return;
            Controller.animController.SetAnim(newAnimState);
            Controller.currentAnimState = newAnimState; // Update the current state
        }

        private void Despawn()
        {
            LeanPool.Despawn(this.gameObject);
        }
    }
}
