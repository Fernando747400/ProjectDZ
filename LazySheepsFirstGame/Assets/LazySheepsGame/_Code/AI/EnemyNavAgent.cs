// Creado Raymundo Mosqueda 07/09/23

using Lean.Pool;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(IdleState))]
    [RequireComponent(typeof(WanderingState))]
    [RequireComponent(typeof(AlertState))]
    [RequireComponent(typeof(AggroState))]
    public class EnemyNavAgent : MonoBehaviour
    {
        public bool DoChase{ get; set; }
        public EnemyParameters Parameters { get; set; }
        public Transform agentTransform;
        [HideInInspector] public NPC_TickManager tickManager;
        [HideInInspector] public EnemyState currentState;
        [HideInInspector] public NavMeshAgent agent;
        
        [SerializeField] private GameObject target;
        [SerializeField] private EnemyParameters parameters;

        private bool _doChase;
        private float _hP; 
        private IdleState _idleState;
        private WanderingState _wanderingState;
        private AlertState _alertState;
        private AggroState _aggroState;
        
    private void Start()
        {
            Prepare();
            currentState = _wanderingState;
            currentState.EnterState();
        }

        private void Update()
        {
            agentTransform = transform;
            currentState.UpdateState();
            if (_hP > 0) return;
            Die();
        }

        private void OnGeometryChanged()
        {
            
        }

        private void Die()
        {
            // LeanPool.Despawn(this);
        }

        private void Prepare()
        {
            agent = GetComponent<NavMeshAgent>();
            tickManager = FindObjectOfType<NPC_TickManager>();
            _idleState = GetComponent<IdleState>();
            _idleState.Agent = this;
            _wanderingState = GetComponent<WanderingState>();
            _wanderingState.Agent = this;
            _alertState = GetComponent<AlertState>();
            _alertState.Agent = this;
            _aggroState = GetComponent<AggroState>();
            _aggroState.Agent = this;
        }
    }
}
