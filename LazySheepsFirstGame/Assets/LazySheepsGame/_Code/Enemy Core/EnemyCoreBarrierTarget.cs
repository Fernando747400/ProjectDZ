using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

public class EnemyCoreBarrierTarget : MonoBehaviour, IGeneralTarget
{
    private Vector3 _currentPosition;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
    {
        throw new System.NotImplementedException();
    }
    
    private void HandleHitPoint()
    {
        throw new System.NotImplementedException();
    }
}
