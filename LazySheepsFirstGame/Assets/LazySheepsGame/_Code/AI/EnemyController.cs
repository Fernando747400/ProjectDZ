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
    public class EnemyController : MonoBehaviour, IGeneralTarget
    {
        [HideInInspector] public NavMeshAgent agent;
        public EnemyParameters Parameters { get; set; }
        public EnemyParameters parameters;
        public GameObject player;
        [HideInInspector] public EnemyState currentState;
        [HideInInspector] public Vector3 target;
        [HideInInspector] public WanderingState wanderingState;
        [HideInInspector] public AlertState alertState;
        [HideInInspector] public AggroState aggroState;
        [HideInInspector] public DeadState deadState;
        [HideInInspector] public float hP;

        private bool _doChase;
        public NPC_TickManager tickManager;

        private void Start()
        {
            Prepare();
            currentState = wanderingState;
            currentState.EnterState();
        }

        private void Update()
        {
            currentState.UpdateState();
            if (hP > 0) return;
            currentState = deadState;
        }

        public void ChangeState(EnemyState newState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }

        private void OnGeometryChanged()
        {

        }

        private void Prepare()
        {
            agent = GetComponent<NavMeshAgent>();
            tickManager = FindObjectOfType<NPC_TickManager>();

            GetStates();
            Parameters = parameters;
            hP = parameters.maxHp;
            agent.speed = parameters.baseSpeed;
            agent.stoppingDistance = parameters.stopDist;
        }

        private void GetStates()
        {
            wanderingState = GetComponent<WanderingState>();
            wanderingState.Controller = this;
            alertState = GetComponent<AlertState>();
            alertState.Controller = this;
            aggroState = GetComponent<AggroState>();
            aggroState.Controller = this;
            deadState = GetComponent<DeadState>();
            deadState.Controller = this;
        }

        public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
        {
            hP -= dmg;
            Debug.Log("Received damage :" + dmg);
        }
    }
}
