// Creado Raymundo Mosqueda 07/09/23

using Lean.Pool;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(WanderingState))]
    [RequireComponent(typeof(AlertState))]
    [RequireComponent(typeof(AggroState))]
    [RequireComponent(typeof(DeadState))]
    public class EnemyController : MonoBehaviour
    {
        [HideInInspector] public EnemyState currentState;
        [HideInInspector] public NavMeshAgent agent;
        public EnemyParameters Parameters { get; set; }
        public EnemyParameters parameters;
        
        [SerializeField] private GameObject target;

        private bool _doChase;
        private float _hP; 
        [HideInInspector] public WanderingState wanderingState;
        [HideInInspector] public AlertState alertState;
        [HideInInspector] public AggroState aggroState;
        [HideInInspector] public DeadState deadState;
       
    private void Start()
        {
            Prepare();
            currentState = wanderingState;
            currentState.EnterState();
        }

        private void Update()
        {
            currentState.UpdateState();
            if (_hP > 0) return;
            currentState = deadState;
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
            wanderingState = GetComponent<WanderingState>();
            wanderingState.Agent = this;
            alertState = GetComponent<AlertState>();
            alertState.Agent = this;
            aggroState = GetComponent<AggroState>();
            aggroState.Agent = this;
            deadState = GetComponent<DeadState>();
            deadState.Agent = this;
        }
    }
}
