using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using CryoStorage;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC_Wander : MonoBehaviour
{
    1#region Wander Variables
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

    #region Debugging Variables
        [Header("Debugging Variables")]
        [Tooltip("Enables visualisation of steering variables. Only works in play mode")]
        [SerializeField]private bool visualize;
    #endregion

    private float _wanderAngle;
    private Vector3 _deviation;

    private float _elapsedTime;
    private float _actTime;
    
    private NavMeshAgent _agent;

    private NPC_TickManager _tickManager;

    [HideInInspector]public bool doWalk;
    private void Awake()
    {
        Prepare();
        doWalk = true;
        _agent.speed = minWalkSpeed;
        _wanderAngle = Random.Range(0, 360f);
    }

    private void Start()
    {
        _tickManager.OnTick += TickManagerOnTick;
    }
    
    private void Update()
    {
        CountTime();
        Visualize();
        if (!doWalk) return;
        _agent.SetDestination(Wander());
    }

    private void TickManagerOnTick(object sender, EventArgs e)
    {
        TickWalkState();
    }

    public void ForceStop()
    {
        doWalk = false;
        _agent.speed = 0;
    }

    public void ForceWalk()
    {
        doWalk = true;
        _agent.speed = Random.Range(minWalkSpeed, maxWalkSpeed);
    }

    public void ForceWait(float waitTime)
    {
        ForceStop();
        Invoke(nameof(ForceWalk), waitTime);
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

    #region Debugging & Visualizing
    private void Visualize()
    {
        if (!visualize) return;
        Debug.DrawLine(transform.position, GetCircleCenter(), Color.magenta);
    }
    
    private void OnDrawGizmos()
    {
        if (!visualize) return;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(_deviation,.1f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(GetCircleCenter(), circleRadius);
    }
    #endregion

    private Vector3 GetCircleCenter()
    {
        Vector3 result = (_agent.velocity.normalized * circleOffset) + transform.position;
        return result;
    }

    private Vector3 Wander()
    {
        float deviationForce = Random.Range(-deviationRange, deviationRange);
        _wanderAngle += deviationForce;
        _deviation = CryoMath.PointOnRadius(GetCircleCenter(), circleRadius, _wanderAngle);
        return _deviation;
    }
    
    private void Prepare()
    {
        if (_agent != null) return;
        try
        {
            _agent = GetComponent<NavMeshAgent>();
        }catch { Debug.LogWarning($"{gameObject.name} could not find NavMeshAgent"); }
        
        if (_tickManager != null) return;
        try
        {
            _tickManager = FindObjectOfType<NPC_TickManager>();
        }catch { Debug.LogWarning(gameObject.name + " Could not find NPC_TickManager"); }

        _actTime = Random.Range(minActTime, maxActTime);
    }
}
