using com.LazyGames.Dio;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [Header("Dependencies Layers")]
    [Tooltip("This layer mask is used to determine if there's valid ground to place the building")]
    [SerializeField] private LayerMask _groundLayerMask;

    [Tooltip("This layer is used to determine collision with other GameObjects that have a Collider")]
    [SerializeField] private LayerMask _buildingsLayerMask;

    [Header("Dependencies Building")]
    [Tooltip("This is the Gameobject that will be instanciated")]
    [SerializeField] private GameObject _objectToBuild;
    [SerializeField] private GameObject _rayCastOrigin;

    [Header("Dependencies Scriptable Objects")]
    [SerializeField] private VoidEventChannelSO _hammerCollisionEvent;

    [Header("BuildMaterials")]
    [SerializeField] private Material _validPlacementMaterial;
    [SerializeField] private Material _invalidPlacementMaterials;

    public bool VRConfirmation {  set { _VRBuildConfirmartion = value; } }

    private RaycastHit _rayHit;
    private bool _canBuild = false;
    private bool _VRBuildConfirmartion = true;

    private Vector3 _buildPosition = Vector3.zero;

    private GameObject _currentGameObject;
    private BuildingCollisionChecker _buildChecker;


    private void OnEnable()
    {
        _hammerCollisionEvent.VoidEvent += Build;
    }

    private void OnDisable()
    {
        _hammerCollisionEvent.VoidEvent -= Build;
    }

    private void Update()
    {
        if (!_canBuild || !_VRBuildConfirmartion) return;

        Ray raycast = new Ray(_rayCastOrigin.transform.position, _rayCastOrigin.transform.forward);

        Debug.DrawRay(raycast.origin, raycast.direction * 1000, Color.red);

        if (Physics.Raycast(raycast, out _rayHit, 1000, _groundLayerMask))
        {
            Vector3 objectSize = _currentGameObject.GetComponent<Renderer>().bounds.size;

            _buildPosition = _rayHit.point + Vector3.up * (objectSize.y / 2);

            _currentGameObject.transform.position = _buildPosition;
        }
    }


    public void StartBuilding()
    {
        _canBuild = true;
        if (_currentGameObject == null) _currentGameObject = Instantiate(_objectToBuild);
        _currentGameObject.SetActive(true);
        if (_buildChecker == null) AddCollisionChecker(_currentGameObject);
        _buildChecker = _currentGameObject.GetComponent<BuildingCollisionChecker>();
        _buildChecker.HammerCollisionEvent = _hammerCollisionEvent;
        _buildChecker.ValidPlacementMaterial = _validPlacementMaterial;
        _buildChecker.InvalidPlacementMaterial = _invalidPlacementMaterials;
        _buildChecker.BuildingsLayerMask= _buildingsLayerMask;
    }

    private void AddCollisionChecker(GameObject gameObject)
    {
        if(gameObject.GetComponent<BuildingCollisionChecker>() == null) 
        {
            gameObject.AddComponent<BuildingCollisionChecker>();
        }
    }

    public void Build()
    {
        if (_buildChecker.IsColliding) return;
        Instantiate(_objectToBuild, _buildPosition, Quaternion.identity);
    }

    public void FinishBuilding()
    {
        _canBuild = false;
        _currentGameObject.SetActive(false);
        Destroy(_currentGameObject);
    }

}


