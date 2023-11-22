using UnityEngine;

namespace com.LazyGames.DZ
{
    public class AlertState : EnemyState
    {
        private float _elapsedTime;
        public override void EnterState()
        {
            //perform alerted animation
            Controller.agent.isStopped = true;
            Controller.doHear = false;
            _elapsedTime = 0f;
        }

        public override void UpdateState()
        {
            //look towards noise source
            transform.LookAt(Controller.target);
            _elapsedTime += Time.fixedDeltaTime;
            FindSource();   
        }
        
        public override void SetAnimation()
        {
            var newAnimState = "";
            
            switch ( Controller.agent.velocity.magnitude)
            {
                case var n when n <= 0.1f:
                    newAnimState = Controller.animDataSo.idleAnim;
                    break;
                case var n when n > 0.1f && n <= 2.1f:
                    newAnimState = Controller.animDataSo.walkAnim;
                    break;
                case var n when n > 2.1f:
                    newAnimState = Controller.animDataSo.runAnim;
                    break;
                default:
                    newAnimState = Controller.animDataSo.idleAnim;
                    break;
            }

            if (newAnimState == Controller.currentAnimState) return;
            Controller.animController.SetAnim(newAnimState);
            Controller.currentAnimState = newAnimState; // Update the current state
        }
        
        private void FindSource()
        {
            var oscillationAngle = Mathf.Sin(Time.time * Controller.parameters.oscillationSpeed) * (Controller.parameters.coneAngle / 2);
            var rayDirection = Quaternion.Euler(0, oscillationAngle, 0) * transform.forward;
            var pos = transform.position;
            Physics.Raycast(pos + Controller.parameters.heightOffset, rayDirection, out var hit, Controller.parameters.softDetectionRange, Physics.DefaultRaycastLayers);
            
            Debug.DrawRay(pos + Controller.parameters.heightOffset, rayDirection * Controller.parameters.softDetectionRange, Color.red);
            
            if (!ReferenceEquals(hit.collider, null))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Controller.ChangeState(Controller.aggroState);
                    return;
                }
            }
            if (_elapsedTime < Controller.parameters.alertTime) return;
            Debug.Log("VAR");
            Controller.ChangeState(Controller.investigatingState);
        }


        public override void ExitState()
        {
            Controller.agent.isStopped = false;
            Controller.doHear = true;

        }
    }
    
}
