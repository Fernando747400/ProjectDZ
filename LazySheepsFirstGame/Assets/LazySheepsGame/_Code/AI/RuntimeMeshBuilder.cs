// Creado Raymundo Mosqueda 24/08/23

using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;


public class RuntimeMeshBuilder : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private float boundsSize = 100f;
    [SerializeField] private GameObject target;
    [SerializeField]private LayerMask layerMask;

    private Vector3 _targetPos;
    private Bounds _bounds;
    private Vector3 _lastAreaPosition;
    private NavMeshData _navData;
    private List<NavMeshBuildSource> _buildSources;


    private void Awake()
    {
        Prepare();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        OnBuildWall();
    }

    private void OnBuildWall()
    {
        surface.BuildNavMesh();
    }
    private void Prepare()
    {
        Vector3 boundsArea = new Vector3(boundsSize, 10, boundsSize);
        _bounds = new Bounds(target.transform.position, boundsArea);
        surface.agentTypeID = NavMesh.GetSettingsByIndex(0).agentTypeID; // Set the agent type
        surface.collectObjects = CollectObjects.Volume;
        surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        surface.layerMask = layerMask;
        surface.center = _bounds.center;
        surface.size = _bounds.size;
        
        surface.BuildNavMesh();
    }

}
