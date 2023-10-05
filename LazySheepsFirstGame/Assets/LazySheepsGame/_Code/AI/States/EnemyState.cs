    // Creado Raymundo Mosqueda 07/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    public abstract class EnemyState : MonoBehaviour
    {
        public EnemyController Controller { get; set; }
        
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        
    }
}
