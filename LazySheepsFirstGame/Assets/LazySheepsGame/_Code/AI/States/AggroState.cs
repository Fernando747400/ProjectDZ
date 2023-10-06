// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    public class AggroState : EnemyState
    {
        public override void EnterState()
        {
            Controller.agent.speed = Controller.parameters.aggroSpeed;
        }

        public override void UpdateState()
        {
            Controller.agent.SetDestination(Controller.player.transform.position);
            CheckDistance();
        }
        
        private void CheckDistance()
        {
            var dist = Vector3.Distance(transform.position, Controller.player.transform.position);
            if(dist > Controller.agent.stoppingDistance) return;
            Attack();
        }

        private void Attack()
        {
            
        }
        
        public override void ExitState()
        {
            // throw new System.NotImplementedException();
        }
    }
    
}
