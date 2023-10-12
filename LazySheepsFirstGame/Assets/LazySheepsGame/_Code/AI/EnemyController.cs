// Creado Raymundo Mosqueda 07/09/23

using System.Collections.Generic;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.DZ
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(WanderingState))]
    [RequireComponent(typeof(AlertState))]
    [RequireComponent(typeof(AggroState))]
    [RequireComponent(typeof(DeadState))]
    public class EnemyController : MonoBehaviour, IGeneralTarget
    {
        public EnemyParameters Parameters { get; set; }
        public EnemyParameters parameters;
        public GameObject player;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public EnemyState currentState;
        [HideInInspector] public Vector3 target;
        [HideInInspector] public WanderingState wanderingState;
        [HideInInspector] public AlertState alertState;
        [HideInInspector] public AggroState aggroState;
        [HideInInspector] public DeadState deadState;
        [HideInInspector] public float hP;
        [HideInInspector] public NPC_TickManager tickManager;
        
        private bool _doChase;
        private List<GameObject> _walls;
        
        public delegate void AdvAnimEventHandler(Vector3 dir);
        public event AdvAnimEventHandler AnimEvent;
        
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
            
            if(agent.pathStatus == NavMeshPathStatus.PathComplete)return;
            Debug.Log("pathparcial");
            // target = 
        }
        
        
        // private GameObject GetClosestWall()
        // {
        //     var dist = 0f;
        //     var currentClosest = 0;
        //     
        //     for (int i = 0; i < _walls.Count; i++)
        //     {
        //         dist = Vector3.Distance(_walls[i].transform.position, player.transform.position);
        //     }
        // }

        private void Prepare()
        {
            agent = GetComponent<NavMeshAgent>();
            tickManager = FindObjectOfType<NPC_TickManager>();

            GetStates();
            Parameters = parameters;
            hP = parameters.maxHp;
            agent.speed = parameters.baseSpeed;
            agent.stoppingDistance = parameters.circleRadius - .1f;
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
            AnimEvent?.Invoke(direction);
            Debug.Log("Received damage :" + dmg);
        }
    }
}
