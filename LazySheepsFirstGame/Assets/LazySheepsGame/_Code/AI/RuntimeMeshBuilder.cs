// Creado Raymundo Mosqueda 24/08/23

using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;
using com.LazyGames.Dio;

namespace com.LazyGames.DZ
{
    public class RuntimeMeshBuilder : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface surface;
        [SerializeField] private float boundsSize = 100f;
        [SerializeField] private GameObject target;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private GameObjectEventChannelSO buildEventChannelSo;
        [SerializeField] private GameObjectEventChannelSO destroyEventChannelSo;
        
        private Vector3 _targetPos;
        private Bounds _bounds;
        private Vector3 _lastAreaPosition;
        private NavMeshData _navData;
        private List<NavMeshBuildSource> _buildSources;


        private void OnEnable()
        {
            buildEventChannelSo.GameObjectEvent += OnGeometryChanged;
            destroyEventChannelSo.GameObjectEvent += OnGeometryChanged;
        }

        private void Awake()
        {
            Prepare();
        }

        private void OnGeometryChanged(GameObject wall)
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
}
