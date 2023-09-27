// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.DZ
{
    public class EnemyNavAgent : MonoBehaviour
    {
        public bool DoChase
        {
            get{ return _doChase;}
            set { _doChase = value; }
        }
        public IdleState idleState;
        public WanderingState wanderingState;
        public AggroState aggroState;
        public AlertState alertState;
        
        [HideInInspector] public EnemyState currentState;

        [SerializeField] private GameObject target;

        private NavMeshAgent _agent;
        private bool _doChase;
    private void Start()
        {
            Prepare();
            currentState = idleState;
        }

        private void Update()
        {
            Motion();
            currentState.UpdateState();
        }

        private void Motion()
        {
            if (!_doChase) return;
        }

        private void PlayerDetection()
        {
            
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
