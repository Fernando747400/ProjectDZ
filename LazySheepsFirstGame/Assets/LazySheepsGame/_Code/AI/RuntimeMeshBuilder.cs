// Creado Raymundo Mosqueda 24/08/23
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;


public class RuntimeMeshBuilder : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private Vector3 boundsSize = new Vector3(20,20,20);
    [SerializeField] private GameObject target;
    [SerializeField]private LayerMask layerMask;

    private Vector3 _targetPos;
    private Bounds _bounds;
    private Vector3 _lastAreaPosition;
    private NavMeshData _navData;
    private List<NavMeshBuildSource> _buildSources;
    

    private void Start()
    {
        Prepare();
    }

    private void BuildNavMesh(bool async)
    {
        surface.BuildNavMesh();
    }
    private void Prepare()
    {
        _bounds = new Bounds(target.transform.position, boundsSize);
        surface.agentTypeID = NavMesh.GetSettingsByIndex(0).agentTypeID; // Set the agent type
        surface.collectObjects = CollectObjects.Volume;
        surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        surface.layerMask = layerMask;
        surface.center = _bounds.center;
        surface.size = _bounds.size;
    }

}
