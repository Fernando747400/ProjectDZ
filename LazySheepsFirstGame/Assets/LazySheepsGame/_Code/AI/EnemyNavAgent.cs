// Creado Raymundo Mosqueda 07/09/23

using Lean.Pool;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    public class EnemyNavAgent : MonoBehaviour
    {
        public bool DoChase{ get; set; }
        public EnemyParameters Parameters { get; set; }
        
        // public IdleState idleState;
        public WanderingState wanderingState;
        // public AggroState aggroState;
        // public AlertState alertState;
        [HideInInspector] public NPC_TickManager tickManager;
        [HideInInspector] public EnemyState currentState;
        [HideInInspector] public NavMeshAgent agent;
        
        [SerializeField] private GameObject target;
        [SerializeField] private EnemyParameters parameters;

        private bool _doChase;
        private float _hP; 
        
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
            Parameters = parameters;
            
            tickManager = FindObjectOfType<NPC_TickManager>();
        }
    }
}
