// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;
using CryoStorage;
using System;
using Random = UnityEngine.Random;

namespace com.LazyGames.DZ
{
    public class WanderingState : EnemyState
    {
        [HideInInspector]public bool doWalk;
        
        #region Wander Variables
        [Header("Movement Variables")]
        [Tooltip("Bottom range of walking speed")]
        [SerializeField]private float minWalkSpeed = 1f;
        [Tooltip("Top range of walking speed")]
        [SerializeField]private float maxWalkSpeed = 3f;
        [Tooltip("Distance of the circle from the _agent")]
        [SerializeField]private float circleOffset = 1.5f;
        [Tooltip("Radius of the circle")]
        [SerializeField]private float circleRadius = 1f;
        [Tooltip("The range that the angle cam move along the circles' diameter")]
        [SerializeField]private float deviationRange = .2f;
        [Tooltip("Bottom range of the time the npc remains still or moving")]
        [SerializeField]private float minActTime = 5f;
        [Tooltip("Top range of the time the npc remains still or moving")]
        [SerializeField]private float maxActTime = 20f;
        #endregion

        #region Detection Variables

        [Header("Detection Variables")]
        [Tooltip("Layer mask of the objects that can be detected")]

        #endregion

        private float _wanderAngle;
        private Vector3 _deviation;
        private float _elapsedTime;
        private float _actTime;

        private NPC_TickManager _tickManager;
        private Transform _agentTransform;
        
        public override void EnterState()
        {
            _tickManager = FindObjectOfType<NPC_TickManager>();
            _tickManager.OnTick += TickManagerOnTick;
        }

        public override void UpdateState()
        {
            _agentTransform = Agent.gameObject.transform;
            PlayerDetection();
            CountTime();
            if (!doWalk) return;
            Agent.agent.SetDestination(Wander());
            
        }

        public override void ExitState()
        {
            
        }
        
        private void PlayerDetection()
        {
            Vector3 offset = new Vector3(0, .5f, 0);
            Debug.DrawRay(transform.position + offset,transform.forward * Agent.parameters.detectionRange, Color.red);

            if(!Physics.Raycast(transform.position + offset, transform.forward, out var hit,Agent.parameters.detectionRange, Physics.DefaultRaycastLayers)) return;
            if (!hit.collider.CompareTag("Player")) return;
            Agent.currentState = Agent.aggroState;
        }

        private void TickManagerOnTick(object sender, EventArgs e)
        {
            TickWalkState();
        }
    
        private void TickWalkState()
        {
            switch (doWalk)
            {
                case false:
                    if (_elapsedTime < _actTime) return;
                    _actTime = Random.Range(minActTime, maxActTime);
                    doWalk = true;
                    _elapsedTime -= _elapsedTime;
                    break;
                case true:
                    if (_elapsedTime < _actTime) return;
                    _actTime = Random.Range(minActTime, maxActTime);
                    doWalk = false;
                    _elapsedTime -= _elapsedTime;
                    break;
            }
        }

        private void CountTime()
        {
            _elapsedTime += Time.fixedDeltaTime;
        }
        
        private Vector3 GetCircleCenter()
        {
            Vector3 result = (Agent.agent.velocity.normalized * circleOffset) + _agentTransform.position;
            return result;
        }

        private Vector3 Wander()
        {
            float deviationForce = Random.Range(-deviationRange, deviationRange);
            _wanderAngle += deviationForce;
            _deviation = CryoMath.PointOnRadius(GetCircleCenter(), circleRadius, _wanderAngle);
            return _deviation;
        }
    }
}
