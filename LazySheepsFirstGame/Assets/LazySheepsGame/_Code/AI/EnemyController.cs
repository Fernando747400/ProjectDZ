// Creado Raymundo Mosqueda 07/09/23

using System;
using com.LazyGames.Dz;
using com.LazyGames.Dio;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(WanderingState))]
    [RequireComponent(typeof(InvestigatingState))]
    [RequireComponent(typeof(AggroState))]
    [RequireComponent(typeof(AlertState))]
    [RequireComponent(typeof(FleeState))]
    [RequireComponent(typeof(DeadState))]
    
    
    public class EnemyController : MonoBehaviour, IGeneralTarget, INoiseSensitive, IGeneralAggressor
    {
        [SerializeField] private VoidEventChannelSO OnCoreDestroyed;
        
        public Collider Collider;
        
        public string playerName = "Auto Hand Player";
        public IntEventChannelSO onDeathScriptableChannel;
        
        public AiParameters parameters;
        public SceneWallsSO sceneWallsSo;

        [HideInInspector] public GameObject player;
        [HideInInspector] public bool doHear = true;
        [HideInInspector] public Vector3 target;
        [HideInInspector] public float hP;
        [HideInInspector]public string currentAnimState;
        
        
        # region "Components"
        
            [HideInInspector] public NavMeshAgent agent;
            [HideInInspector] public NPC_TickManager tickManager;
            [HideInInspector]public AdvanceAnimatorController animController;
        
        # endregion
        
        # region "EnemyStates"
        
            public EnemyState currentState;
            [HideInInspector] public WanderingState wanderingState;
            [HideInInspector] public InvestigatingState investigatingState;
            [HideInInspector] public AggroState aggroState;
            [HideInInspector] public AlertState alertState;
            [HideInInspector] public FleeState fleeState;
            [HideInInspector] public DeadState deadState;
            
        #endregion
        
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
            currentState.SetAnimation();
            if (hP > 0) return;
            ChangeState(deadState);
        }

        public void ChangeState(EnemyState newState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
        
        private void GetStates()
        {
            wanderingState = GetComponent<WanderingState>();
            wanderingState.Controller = this;
            investigatingState = GetComponent<InvestigatingState>();
            investigatingState.Controller = this;
            aggroState = GetComponent<AggroState>();
            aggroState.Controller = this;
            alertState = GetComponent<AlertState>();
            alertState.Controller = this;
            fleeState = GetComponent<FleeState>();
            fleeState.Controller = this;
            deadState = GetComponent<DeadState>();
            deadState.Controller = this;
        }
        
        private void Prepare()
        {
            agent = GetComponent<NavMeshAgent>();
            tickManager = FindObjectOfType<NPC_TickManager>();

            GetStates();
            animController = GetComponent<AdvanceAnimatorController>();
            hP = parameters.maxHp;
            agent.speed = parameters.baseSpeed;
            agent.stoppingDistance = parameters.circleRadius - .1f;
            player = GameObject.Find(playerName);
            
            OnCoreDestroyed.VoidEvent += OnDestroyCore;
        }
        
        public void ReceiveAggression(Vector3 direction, float velocity,float dmg = 0)
        {
            if (currentState == deadState) return;
            hP -= dmg;
            doHear = false;
            target = direction;
            OnAnimEvent?.Invoke(direction);
            ChangeState(alertState);
            // Debug.Log("Received damage :" + dmg);
        }

        public void HearNoise(float intensity, Vector3 position, bool dangerous)
        {
            if (!doHear) return;
            // Debug.Log($"{gameObject.name} heard a noise of intensity {intensity}");
            if (parameters.skittish)
            {
                if(intensity < .1f) return;
                currentState = fleeState;
                fleeState.Source = position;
            }
            else
            {
                switch (intensity)
                {
                    case var n when n > .3f:
                        if (dangerous)
                        {
                            ChangeState(alertState); 
                            target = position;
                        }
                        else
                        {
                            ChangeState(investigatingState); 
                            target = position;
                        }
                        break;
                        
                    case var n when n <= .3f:
                        currentState = investigatingState;
                        target = position;
                        break;
                    
                    case var n when n <= .2f:
                        break;
                }
            }
        }

        public void SendAggression()
        {
            Debug.Log("attacked");
        }

        private void OnDestroyCore()
        {
            ChangeState(deadState);
            Collider.enabled = false;
        }
    }
}

