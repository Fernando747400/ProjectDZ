// Creado Raymundo Mosqueda 07/09/23
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicNavAgent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private NavMeshAgent _agent;

    private void Start()
    {
        Prepare();

    }

    private void Update()
    {
        if (!_agent.isOnNavMesh) return;
        _agent.destination = target.transform.position;
    }

    private void OnGeometryChanged()
    {
        
    }

    private void Prepare()
    {
        _agent = GetComponent<NavMeshAgent>();
        
    }

}
