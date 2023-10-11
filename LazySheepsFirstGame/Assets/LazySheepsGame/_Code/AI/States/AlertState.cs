// Creado Raymundo Mosqueda 07/09/23
using com.LazyGames.DZ;
using UnityEngine;

public class AlertState : EnemyState
{
    
    public override void EnterState()
    {
        Controller.agent.speed = Controller.parameters.alertSpeed;
    }

    public override void UpdateState()
    {
        Controller.agent.SetDestination(Controller.target);
        PlayerDetection();
        float dist = Vector3.Distance(transform.position,Controller.target);
        if (dist <= Controller.agent.stoppingDistance)
        {
            Controller.ChangeState(Controller.wanderingState);
        }
    }   
    
    private void PlayerDetection()
    {
        float oscillationAngle = Mathf.Sin(Time.time * Controller.parameters.oscillationSpeed) * (Controller.parameters.coneAngle / 2);
        Vector3 rayDirection = Quaternion.Euler(0, oscillationAngle, 0) * transform.forward;

        Debug.DrawRay(transform.position + Controller.parameters.heightOffset, rayDirection * Controller.parameters.softDetectionRange, Color.black);
        
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
        }
    }

    public override void ExitState()
    {
        Controller.agent.speed = Controller.parameters.baseSpeed;
    }
}