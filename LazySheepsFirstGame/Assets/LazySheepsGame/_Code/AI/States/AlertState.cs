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
            _elapsedTime = 0f;
        }

        public override void UpdateState()
        {
            //look towards noise source
            transform.LookAt(Controller.target);
            _elapsedTime += Time.fixedDeltaTime;
            Debug.Log(_elapsedTime);
            FindSource();   
        }
        
        private void FindSource()
        {
            var oscillationAngle = Mathf.Sin(Time.time * Controller.parameters.oscillationSpeed) * (Controller.parameters.coneAngle / 2);
            var rayDirection = Quaternion.Euler(0, oscillationAngle, 0) * transform.forward;
            var pos = transform.position;
            var detected = Physics.Raycast(pos + Controller.parameters.heightOffset, rayDirection, out var hit, Controller.parameters.softDetectionRange, Physics.DefaultRaycastLayers);
            if (!detected) return;
            if (hit.collider.CompareTag("Player"))
            {
                Controller.ChangeState(Controller.aggroState);
            }
            else
            {
                if(_elapsedTime < Controller.parameters.alertTime) return;
                Controller.ChangeState(Controller.wanderingState);
            }
        }

        public override void ExitState()
        {
            Controller.agent.isStopped = false;
            
        }
    }
    
}
