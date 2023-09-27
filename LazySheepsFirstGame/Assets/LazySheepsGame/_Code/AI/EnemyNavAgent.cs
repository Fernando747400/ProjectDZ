// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.DZ
{
    public class EnemyNavAgent : MonoBehaviour
    {
        public EnemyState currentState;
        public IdleState idleState;
        public WanderingState wanderingState;
        public AggroState aggroState;
        
        [SerializeField] private GameObject target;
        
        private NavMeshAgent _agent;
        
        private void Start()
        {
            Prepare();

        }

        private void Update()
        {
            if (!_agent.isOnNavMesh) return;
            _agent.destination = target.transform.position;
        }

        private void OnGeometryChanged()
        {
            
        }

        private void Prepare()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

    }
}
