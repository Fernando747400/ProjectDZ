// Creado Raymundo Mosqueda 07/09/23

using System;
using com.LazyGames.Dz;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(WanderingState))]
    [RequireComponent(typeof(InvestigatingState))]
    [RequireComponent(typeof(AggroState))]
    [RequireComponent(typeof(DeadState))]
    public class EnemyController : MonoBehaviour, IGeneralTarget, INoiseSensitive
    {
        public EnemyParameters Parameters { get; set; }
        public EnemyParameters parameters;
        public SceneWallsSO sceneWallsSo;
        public GameObject player;
        
        [HideInInspector]public bool doHear = true;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public EnemyState currentState;
        [HideInInspector] public Vector3 target;
        [HideInInspector] public WanderingState wanderingState;
        [HideInInspector] public InvestigatingState investigatingState;
        [HideInInspector] public AggroState aggroState;
        [HideInInspector] public DeadState deadState;
        [HideInInspector] public float hP;
        [HideInInspector] public NPC_TickManager tickManager;
        
        public event Action<Vector3> OnAnimEvent;
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
            investigatingState = GetComponent<InvestigatingState>();
            investigatingState.Controller = this;
            aggroState = GetComponent<AggroState>();
            aggroState.Controller = this;
            deadState = GetComponent<DeadState>();
            deadState.Controller = this;
        }

        public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
        {
            hP -= dmg;
            OnAnimEvent?.Invoke(direction);
            Debug.Log("Received damage :" + dmg);
        }

        public void HearNoise(float intensity, Vector3 position, bool dangerous)
        {
            if (!doHear) return;
            Debug.Log($"heardNoise {intensity}");
            if (intensity > .2f)
            {
                currentState = investigatingState;
                target = position;
            }
            // else
            // {
            //     currentState = //dangerous ? aggroState : 
            //         investigatingState;
            // }
        }
    }
}
