// Creado Raymundo Mosqueda 04/10/23

namespace com.LazyGames.DZ
{
    public class DeadState : EnemyState
    {
        public override void EnterState()
        {
            Controller.agent.speed = 0;
        }
        
        public override void UpdateState()
        {
            
        }
        
        public override void ExitState()
        {
            
        }
    }
}
