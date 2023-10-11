// Creado Raymundo Mosqueda 04/10/23

using Lean.Pool;

namespace com.LazyGames.DZ
{
    public class DeadState : EnemyState
    {
        public override void EnterState()
        {
            Controller.agent.speed = 0;
            LeanPool.Despawn(this.gameObject);
        }
        
        public override void UpdateState()
        {
            
        }
        
        public override void ExitState()
        {
            Controller.hP = Controller.parameters.maxHp;
            Controller.agent.speed = Controller.parameters.baseSpeed;
        }
    }
}
