// Creado Raymundo Mosqueda 30/10/23

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoStorage;


namespace com.LazyGames.DZ
{
    public class FleeState : EnemyState
    {
        public Vector3 Source { get; set; }

        private float _angleToSource;
        private float _distanceToSource;
        public override void EnterState()
        {
            // Controller.doHear = false;
            Controller.agent.speed = Controller.parameters.fleeSpeed;
        }

        public override void UpdateState()
        {
            Debug.Log($"{gameObject.name} is fleeing");
            _angleToSource = Vector3.Angle(Source, transform.position);
            _distanceToSource = Vector3.Distance(Source, transform.position);
            if (_distanceToSource >= Controller.parameters.deAggroDistance)
            {
                Controller.agent.SetDestination(CryoMath.PointOnRadius(transform.position, 60, _angleToSource));
            }
            // Controller.target = CryoMath.PointOnRadius(transform.position, 100,)
        }

        public override void ExitState()
        {
            Controller.doHear = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Debug.DrawLine(transform.position, CryoMath.PointOnRadius(transform.position, 60, _angleToSource), Color.green);
            Gizmos.DrawSphere(CryoMath.PointOnRadius(transform.position, 60, _angleToSource) , .3f);
        }
    }
}
