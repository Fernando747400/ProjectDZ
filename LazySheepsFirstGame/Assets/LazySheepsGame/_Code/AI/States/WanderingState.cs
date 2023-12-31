// Creado Raymundo Mosqueda 07/09/23
using UnityEngine;
using CryoStorage;
using System;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace com.LazyGames.DZ
{
    public class WanderingState : EnemyState
    {
        [HideInInspector]public bool doWalk;    

        private float _wanderAngle;
        private Vector3 _deviation;
        private float _elapsedTime;
        private float _actTime;
        private bool _detected;
        
        private Transform _agentTransform;
        
        public override void EnterState()
        {
            Controller.tickManager.OnTick += TickManagerOnTick;
            // _wanderAngle = 180f; //aligns the initial vector to the front of the enemy
        }

        public override void UpdateState()
        {
            _agentTransform = Controller.gameObject.transform;
            PlayerDetection(CastRay());
            Avoidance(CastRay());
            CountTime();
            if (!doWalk) return;
            Controller.agent.SetDestination(Wander());
        }

        public override void ExitState()
        {
            Controller.tickManager.OnTick -= TickManagerOnTick;
        }

        private RaycastHit CastRay()
        {
            float oscillationAngle = Mathf.Sin(Time.time * Controller.parameters.oscillationSpeed) * (Controller.parameters.coneAngle / 2);
            Vector3 rayDirection = Quaternion.Euler(0, oscillationAngle, 0) * transform.forward;
            var pos = transform.position;
            _detected = Physics.Raycast(pos + Controller.parameters.heightOffset, rayDirection, 
                out var hit, Controller.parameters.softDetectionRange, Physics.DefaultRaycastLayers);
            
            Debug.DrawRay(pos + Controller.parameters.heightOffset, rayDirection * Controller.parameters.hardDetectionRange, Color.red);
            Debug.DrawRay((pos + Controller.parameters.heightOffset) +
            rayDirection * Controller.parameters.hardDetectionRange , rayDirection * Controller.parameters.softDetectionRange, Color.yellow);
            return hit;
        }

        private void PlayerDetection(RaycastHit hit)
        {
            if (!_detected) return;
            if (!hit.collider.CompareTag("Player")) return;
            if (hit.distance <= Controller.parameters.hardDetectionRange)
            {
                Controller.ChangeState(Controller.aggroState);
            }
            else
            {
                Controller.target = hit.collider.transform.position;
                Controller.ChangeState(Controller.investigatingState);
            }
        }
        
        private void Avoidance(RaycastHit hit)
        {
            if(!_detected) return;
            if (hit.distance <= Controller.parameters.hardDetectionRange) return;
                
            _deviation *= 10;
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
                    _actTime = Random.Range(Controller.parameters.minActTime, Controller.parameters.maxActTime);
                    doWalk = true;
                    _elapsedTime -= _elapsedTime;
                    break;
                case true:
                    if (_elapsedTime < _actTime) return;
                    _actTime = Random.Range(Controller.parameters.minActTime, Controller.parameters.maxActTime);
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
            Vector3 result = (Controller.agent.velocity.normalized * Controller.parameters.circleRadius) + _agentTransform.position;
            return result;
        }
        
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
                                                                            
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(_deviation,.1f);
            Gizmos.color = Color.magenta;
            Debug.DrawLine(transform.position, GetCircleCenter(), Color.magenta);
            Gizmos.DrawWireSphere(GetCircleCenter(), Controller.parameters.circleRadius);
        }

        private Vector3 Wander()
        {
            float deviationForce = Random.Range(Controller.parameters.deviationRange * -1, Controller.parameters.deviationRange);
            _wanderAngle += deviationForce;
            _deviation = CryoMath.PointOnRadius(GetCircleCenter(), Controller.parameters.circleRadius, _wanderAngle);
            return _deviation;
        }
    }
}
