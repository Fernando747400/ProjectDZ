    // Creado Raymundo Mosqueda 07/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    public abstract class EnemyState : ScriptableObject
    {
        protected EnemyNavAgent _agent;
        public float speed = 1f;

        protected EnemyState (EnemyNavAgent agent)
        {
            _agent = agent;
        }
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        
    }
}
