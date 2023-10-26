using com.LazyGames;
using com.LazyGames.Dio;
using NaughtyAttributes;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BuildingSystem : MonoBehaviour, IPausable
{
    [Header("Dependencies Layers")]
    [Tooltip("This layer mask is used to determine if there's valid ground to place the building")]
    [SerializeField] private LayerMask _groundLayerMask;

    [Tooltip("This layer is used to determine collision with other GameObjects that have a Collider")]
    [SerializeField] private LayerMask _buildingsLayerMask;

    [Header("Dependencies Building")]
    [Tooltip("This is the Gameobject that will be instanciated")]
    [Required]
    [SerializeField] private GameObject _objectToBuild;
    [Required]
    [SerializeField] private GameObject _rayCastOrigin;

    [Header("Dependencies Scriptable Objects")]
    [Required]
    [SerializeField] private BoolEventChannelSO _pauseEventChannel;
    [Required]
    [SerializeField] private VoidEventChannelSO _hammerCollisionEvent;
    [Required]
    [SerializeField] private GameObjectEventChannelSO _buildEventChannel;

    [Header("BuildMaterials")]
    [SerializeField] private Material _validPlacementMaterial;
    [SerializeField] private Material _invalidPlacementMaterials;

    public BoolEventChannelSO PauseEventChannel { get; set; }
    public bool VRConfirmation { set { _VRBuildConfirmartion = value; } }

    [SerializeField] public BoolEventChannelSO PauseUpdateChannel { get { return _pauseEventChannel; } set { _pauseEventChannel = value; } }
    [HideInInspector] public bool IsPaused { get; set; }

    private RaycastHit _rayHit;
    private bool _canBuild = false;
    private bool _VRBuildConfirmartion = false;

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

    private void Start()
    {
        Prepare();
    }

    private void Prepare()
    {
        if (_currentGameObject == null) _currentGameObject = Instantiate(_objectToBuild);
        FinishBuilding();
        _VRBuildConfirmartion = false;
    }

    private void Update()
    {
        if (!_canBuild || !_VRBuildConfirmartion) return;

        Ray raycast = new Ray(_rayCastOrigin.transform.position, _rayCastOrigin.transform.forward);

        Debug.DrawRay(raycast.origin, raycast.direction * 1000, Color.red);

        if (Physics.Raycast(raycast, out _rayHit, 1000, _groundLayerMask))
        {
            Vector3 offset = _rayHit.normal * 0.1f;

            _buildPosition = _rayHit.point + offset;
            _currentGameObject.transform.position = _buildPosition;

            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, _rayHit.normal);
            _currentGameObject.transform.rotation = rotation;
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


    public void Build()
    {
        if (_buildChecker.IsColliding) return;
       GameObject building = Instantiate(_objectToBuild, _buildPosition, _currentGameObject.transform.rotation);
       BuildShader(building);
       _buildEventChannel.RaiseEvent(building);
    }

    public void FinishBuilding()
    {
        _canBuild = false;
        _currentGameObject.SetActive(false);
    }
    private void AddCollisionChecker(GameObject gameObject)
    {
        if(gameObject.GetComponent<BuildingCollisionChecker>() == null) 
        {
            gameObject.AddComponent<BuildingCollisionChecker>();
        }
    }

    private void BuildShader(GameObject building)
    {
        if (building.GetComponent<BuildShaderManager>() == null) building.AddComponent<BuildShaderManager>();
        BuildShaderManager buildShaderScript = building.GetComponent<BuildShaderManager>();
        buildShaderScript.StartBuildEffect();
    }

}


