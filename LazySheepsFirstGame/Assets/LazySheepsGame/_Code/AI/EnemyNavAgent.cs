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
        
        public IdleState idleState;
        public WanderingState wanderingState;
        public AggroState aggroState;
        public AlertState alertState;
        [HideInInspector] public EnemyState currentState;
        
        [SerializeField] private GameObject target;
        [SerializeField] private EnemyParameters parameters;

        private NavMeshAgent _agent;
        private bool _doChase;
        private float _hP; 
        
    private void Start()
        {
            Prepare();
            currentState = idleState;
        }

        private void Update()
        {
            
            Motion();
            currentState.UpdateState();
            if (_hP > 0) return;
            Die();
        }

        private void Motion()
        {
            
        }

      

        private void OnGeometryChanged()
        {
            
        }

        private void Die()
        {
            Debug.Log("isded");
            // LeanPool.Despawn(this);
        }

        private void Prepare()
        {
            _agent = GetComponent<NavMeshAgent>();
            Parameters = parameters;
        }
    }
}
