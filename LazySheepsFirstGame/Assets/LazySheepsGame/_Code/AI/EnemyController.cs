// Creado Raymundo Mosqueda 07/09/23

using Lean.Pool;
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.DZ
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(WanderingState))]
    [RequireComponent(typeof(AlertState))]
    [RequireComponent(typeof(AggroState))]
    public class EnemyNavAgent : MonoBehaviour
    {
        [HideInInspector] public EnemyState currentState;
        [HideInInspector] public NavMeshAgent agent;
        public EnemyParameters Parameters { get; set; }
        public EnemyParameters parameters;
        
        [SerializeField] private GameObject target;

        private bool _doChase;
        private float _hP; 
        private WanderingState _wanderingState;
        private AlertState _alertState;
        private AggroState _aggroState;
        private DeadState _deadState;
        
    private void Start()
        {
            Prepare();
            currentState = _wanderingState;
            currentState.EnterState();
        }

        private void Update()
        {
            currentState.UpdateState();
            if (_hP > 0) return;
            currentState = _deadState;
        }

        private void OnGeometryChanged()
        {
            
        }
        
        private void Prepare()
        {
            agent = GetComponent<NavMeshAgent>();
            GetStates();
            Parameters = parameters;
            _hP = parameters.maxHp;
            agent.speed = parameters.moveSpeed;
        }

        private void GetStates()
        {
            _wanderingState = GetComponent<WanderingState>();
            _wanderingState.Agent = this;
            _alertState = GetComponent<AlertState>();
            _alertState.Agent = this;
            _aggroState = GetComponent<AggroState>();
            _aggroState.Agent = this;
            _deadState = GetComponent<DeadState>();
            _deadState.Agent = this;
        }
    }
}
