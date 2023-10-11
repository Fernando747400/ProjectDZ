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

        private float _wanderAngle;
        private Vector3 _deviation;
        private float _elapsedTime;
        private float _actTime;
        
        private Transform _agentTransform;
        
        public override void EnterState()
        {
            Controller.tickManager.OnTick += TickManagerOnTick;
        }

        public override void UpdateState()
        {
            _agentTransform = Controller.gameObject.transform;
            PlayerDetection();
            CountTime();
            if (!doWalk) return;
            Controller.agent.SetDestination(Wander());
        }

        public override void ExitState()
        {
            Controller.tickManager.OnTick -= TickManagerOnTick;
        }
        


        private void PlayerDetection()
        {
            float oscillationAngle = Mathf.Sin(Time.time * Controller.parameters.oscillationSpeed) * (Controller.parameters.coneAngle / 2);

            Vector3 rayDirection = Quaternion.Euler(0, oscillationAngle, 0) * transform.forward;

            Debug.DrawRay(transform.position + Controller.parameters.heightOffset, rayDirection * Controller.parameters.softDetectionRange, Color.white);

            if (!Physics.Raycast(transform.position + Controller.parameters.heightOffset, rayDirection,
                    out var hit, Controller.parameters.softDetectionRange, Physics.DefaultRaycastLayers)) return;
            if (!hit.collider.CompareTag("Player")) return;
            if (hit.distance <= Controller.parameters.hardDetectionRange)
            {
                Debug.DrawRay(transform.position + Controller.parameters.heightOffset, rayDirection * Controller.parameters.softDetectionRange, Color.red);
                Controller.ChangeState(Controller.aggroState);
            }
            else
            {
                Debug.DrawRay(transform.position + Controller.parameters.heightOffset, rayDirection * Controller.parameters.softDetectionRange, Color.yellow);
                Controller.target = hit.collider.transform.position;
                Controller.ChangeState(Controller.alertState);
            }
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
            Vector3 result = (Controller.agent.velocity.normalized * circleOffset) + _agentTransform.position;
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
