// Creado Raymundo Mosqueda 30/10/23

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoStorage;
using UnityEditor;


namespace com.LazyGames.DZ
{
    public class FleeState : EnemyState
    {
        public Vector3 Source { get; set; }

        private float _angleToSource;
        private float _distanceToSource;
        public override void EnterState()
        {
            Controller.doHear = false;
            Controller.agent.speed = Controller.parameters.fleeSpeed;
        }

        public override void UpdateState()
        {
            _angleToSource = Vector3.Angle(Source, transform.position) + 180f;
            _distanceToSource = Vector3.Distance(Source, transform.position);
            if (_distanceToSource >= Controller.parameters.deAggroDistance)
            {
                Controller.agent.SetDestination(CryoMath.PointOnRadius(transform.position, Controller.parameters.circleRadius, _angleToSource));
            }
            if (_distanceToSource <= Controller.parameters.deAggroDistance) return;
            Controller.ChangeState(Controller.wanderingState);
            
        }

        public override void ExitState()
        {
            Controller.doHear = true;
        }
        
        public override void SetAnimation()
        {
            var newAnimState = "";
            
            switch ( Controller.agent.velocity.magnitude)
            {
                case var n when n <= 0.1f:
                    newAnimState = "Idle";
                    break;
                case var n when n > 0.1f && n <= 2.1f:
                    newAnimState = "Walking";
                    break;
                case var n when n > 2.1f:
                    newAnimState = "Running";
                    break;
                default:
                    newAnimState = "Idle";
                    break;
            }

            if (newAnimState == Controller.currentAnimState) return;
            Controller.animController.SetAnim(newAnimState);
            Controller.currentAnimState = newAnimState; // Update the current state
        }
    }
}
