using com.LazyGames.Dio;
using NaughtyAttributes;
using Unity.VisualScripting;
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
    [Required]
    [SerializeField] private GameObject _prefabToBuild;
    [Required]
    [SerializeField] private GameObject _rayCastOrigin;



    [Header("Scriptable Objects Channel")]

    [Required]
    [SerializeField] private VoidEventChannelSO _hammerCollisionChannel;
    [Required]
    [SerializeField] private GameObjectEventChannelSO _buildEventChannel;
    [Required]
    [SerializeField] private GameObjectEventChannelSO _wallDestroyedEventChannel;
    [Required]
    [SerializeField] private BoolEventChannelSO _xrConfirmationChannel;
    [Required]
    [SerializeField] private BoolEventChannelSO _pauseEventChannel;



    [Header("BuildMaterials")]

    [SerializeField] private Material _validPlacementMaterial;
    [SerializeField] private Material _invalidPlacementMaterials;
    [SerializeField] private Material _cooldownPlacementMaterial;

    [Header("Settings")]
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private int _maxWalls = 3;
    [SerializeField] private float _cooldown = 30.0f;

    private RaycastHit _rayHit;
    private Vector3 _buildPosition = Vector3.zero;
    
    private GameObject _currentGameObjectReference;
    private BuildingCollisionChecker _buildChecker;
    
    private bool _canBuild = false;
    private bool _xrConfirmation = false;
    private bool _isPaused = false;
    private bool _onCooldown = false;
    private float _currentElapsedCooldown;
    private int _currentWalls = 0;


    private void OnEnable()
    {
        _hammerCollisionChannel.VoidEvent += BuildObject;
        _xrConfirmationChannel.BoolEvent += UpdateXRConfirmation;
        _pauseEventChannel.BoolEvent += UpdatePause;
        _wallDestroyedEventChannel.GameObjectEvent += WallDestroyedEvent;
    }

    private void OnDisable()
    {
        _hammerCollisionChannel.VoidEvent -= BuildObject;
        _xrConfirmationChannel.BoolEvent -= UpdateXRConfirmation;
        _pauseEventChannel.BoolEvent -= UpdatePause;
        _wallDestroyedEventChannel.GameObjectEvent -= WallDestroyedEvent;
    }

    private void Awake()
    {
        Prepare();
    }

    private void Start()
    {

    }

    private void Prepare()
    {
        if (_currentGameObjectReference == null) _currentGameObjectReference = Instantiate(_prefabToBuild);
        _currentGameObjectReference.SetActive(false);
        FinishBuilding();
        _xrConfirmation = false;       
    }

    private void Update()
    {
        UpdateCooldown();
        DoBuild();
    }

    public void StartBuilding()
    {
        _canBuild = true;
        if (_currentGameObjectReference == null) _currentGameObjectReference = Instantiate(_prefabToBuild);
        _currentGameObjectReference.SetActive(true);

        AddCollisionChecker(_currentGameObjectReference);

        //pass build checker parameters
        _buildChecker.HammerCollisionEvent = _hammerCollisionChannel;
        _buildChecker.ValidPlacementMaterial = _validPlacementMaterial;
        _buildChecker.InvalidPlacementMaterial = _invalidPlacementMaterials;
        _buildChecker.CooldownMaterial = _cooldownPlacementMaterial;
        _buildChecker.BuildingsLayerMask= _buildingsLayerMask;
    }


    public void BuildObject()
    {
        if (_buildChecker.IsColliding || _onCooldown) return;
       GameObject building = Instantiate(_prefabToBuild, _buildPosition, _currentGameObjectReference.transform.rotation);
       //BuildShader(building);
       building.GetComponent<BoxCollider>().isTrigger = false;
       _buildEventChannel.RaiseEvent(building);
        _currentWalls++;
       _onCooldown = true;
    }

    public void FinishBuilding()
    {
        _canBuild = false;
        _currentGameObjectReference.SetActive(false);
    }

    private void DoBuild()
    {
        if (!_canBuild || !_xrConfirmation || _isPaused || _currentWalls >= _maxWalls) return;

        Ray raycast = new Ray(_rayCastOrigin.transform.position, _rayCastOrigin.transform.forward);

        Debug.DrawRay(raycast.origin, raycast.direction * _maxDistance, Color.red);

        if (Physics.Raycast(raycast, out _rayHit, _maxDistance, _groundLayerMask))
        {
            Vector3 offset = _rayHit.normal * 0.1f;

            _buildPosition = _rayHit.point + offset;
            _currentGameObjectReference.transform.position = _buildPosition;

            // Calculate the rotation to align with the floor normal
            Quaternion floorRotation = Quaternion.FromToRotation(Vector3.up, _rayHit.normal);

            // Calculate the rotation to align the Y-axis with the direction to the player
            Vector3 playerDirection = (_rayCastOrigin.transform.position - _currentGameObjectReference.transform.position).normalized;
            float angle = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg + 180f;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

            // Apply the floor rotation and add the rotation around the y-axis to face the player
            _currentGameObjectReference.transform.rotation = floorRotation * Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
    }





    private void UpdateCooldown()
    {
        if (_onCooldown)
        {
            _currentElapsedCooldown += Time.deltaTime;
            if (_currentElapsedCooldown >= _cooldown)
            {
                _currentElapsedCooldown = 0.0f;
                _onCooldown = false;
            }          
        }

        if (_buildChecker != null) _buildChecker.OnCooldown = _onCooldown;
    }

    private void AddCollisionChecker(GameObject gameObject)
    {
        _buildChecker = gameObject.GetOrAddComponent<BuildingCollisionChecker>();
    }

    private void WallDestroyedEvent(GameObject destroyedWall)
    {
        _currentWalls--;
    }

    private void BuildShader(GameObject building)
    {
        BuildShaderManager buildShaderScript = building.GetOrAddComponent<BuildShaderManager>();
        buildShaderScript.StartBuildEffect();
    }

    private void UpdateXRConfirmation(bool value)
    {
        _xrConfirmation = value;
    }

    private void UpdatePause(bool value)
    {
        _isPaused = value;
    }

}


