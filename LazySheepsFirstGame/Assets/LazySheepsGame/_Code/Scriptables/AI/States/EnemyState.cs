    // Creado Raymundo Mosqueda 07/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    public abstract class EnemyState : ScriptableObject
    {
        protected EnemyNavAgent Agent;

        protected EnemyState (EnemyNavAgent agent)
        {
            Agent = agent;
        }
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        
    }
}
