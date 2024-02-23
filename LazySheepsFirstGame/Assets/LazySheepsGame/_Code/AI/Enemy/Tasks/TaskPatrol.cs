using com.LazyGames.DZ;
using UnityEngine;
using com.LazyGames.Dz.Ai;

public class TaskPatrol : Node
{
    [SerializeField] private AiParameters _parameters;
    [Tooltip("Time in seconds to wait at each waypoint")]
    [SerializeField]private float waitTime = 0;
    
    private Transform _transform;
    private Transform[] _wayPoints;
    private int _currentWayPoint = 0;

    private float _waitcounter = 0;
    private bool _waiting = false;
    
    public TaskPatrol(Transform transform, Transform[] wayPoints)
    {
        _transform = transform;
        _wayPoints = wayPoints;
    }
    
    public override NodeStates Evaluate()
    {
        if (_waiting)
        {
            _waitcounter = Time.deltaTime;
            if (_waitcounter >= waitTime)
                _waiting = false;
        }
        else
        {
            Transform wp = _wayPoints[_currentWayPoint];
            if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                _waitcounter = 0;
                _waiting = true;

                _currentWayPoint = _currentWayPoint ++ % (_wayPoints.Length);
            }
            else
            {
                var pos = wp.position;
                _transform.position = Vector3.MoveTowards(_transform.position,
                    pos, _parameters.patrolSpeed * Time.deltaTime);
                _transform.LookAt(pos);
            }
        }
        
        state = NodeStates.Running;
        return state;
    }
}

