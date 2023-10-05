// Creado Raymundo Mosqueda 07/09/23
using com.LazyGames.DZ;
using UnityEngine;

public class AlertState : EnemyState
{
    private Vector3 _targetPos;
    
    #region Detection Variables
    [Header("Detection Variables")]
    [Tooltip("Layer mask of the objects that can be detected")]
    private float _oscillationSpeed = 50f;
    private float _coneAngle = 45f;
    private Vector3 _offset = new Vector3(0, .5f, 0);
    #endregion
    public override void EnterState()
    {
        _targetPos = Controller.target;
        Controller.agent.speed = Controller.parameters.alertSpeed;
    }

    public override void UpdateState()
    {
        Controller.agent.SetDestination(Controller.target);
        PlayerDetection();
        float dist = Vector3.Distance(transform.position,Controller.target);
        if (dist < .5f)
        {
            Controller.ChangeState(Controller.wanderingState);
        }
    }
    
    private void PlayerDetection()
    {
        float oscillationAngle = Mathf.Sin(Time.time * _oscillationSpeed) * (_coneAngle / 2);

        Vector3 rayDirection = Quaternion.Euler(0, oscillationAngle, 0) * transform.forward;
        
        if (Physics.Raycast(transform.position + _offset, rayDirection, out var hit, Controller.parameters.softDetectionRange, Physics.DefaultRaycastLayers))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (hit.distance <= Controller.parameters.hardDetectionRange)
                {
                    Debug.DrawRay(transform.position + _offset, rayDirection * Controller.parameters.softDetectionRange, Color.red);
                    Controller.ChangeState(Controller.aggroState);
                }
                else
                {
                    Debug.DrawRay(transform.position + _offset, rayDirection * Controller.parameters.softDetectionRange, Color.yellow);
                    Controller.target = hit.collider.transform.position;
                }
            }
        }
    }

    public override void ExitState()
    {
        Controller.agent.speed = Controller.parameters.baseSpeed;
    }
}