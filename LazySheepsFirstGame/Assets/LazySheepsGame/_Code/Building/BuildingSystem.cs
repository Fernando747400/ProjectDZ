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
    [SerializeField] private GameObject _objectToBuild;
    [SerializeField] private GameObject _rayCastOrigin;

    public bool VRConfirmation {  set { _VRBuildConfirmartion = value; } }

    private RaycastHit _rayHit;
    private bool _canBuild = false;
    private bool _VRBuildConfirmartion = true;

    private GameObject _currentGameObject;
    private BuildingCollisionChecker _buildChecker;


    private void Update()
    {
        if (!_canBuild || !_VRBuildConfirmartion) return;

        Ray raycast = new Ray(_rayCastOrigin.transform.position, _rayCastOrigin.transform.forward);

        Debug.DrawRay(raycast.origin, raycast.direction * 1000, Color.red);

        if (Physics.Raycast(raycast, out _rayHit, 1000, _groundLayerMask))
        {
            Vector3 objectSize = _currentGameObject.GetComponent<Renderer>().bounds.size;

            Vector3 newPosition = _rayHit.point + Vector3.up * (objectSize.y / 2);

            _currentGameObject.transform.position = newPosition;

            if (Input.GetMouseButtonDown(0))
            {
                Build(newPosition);
            }
        }
    }


    public void StartBuilding()
    {
        _canBuild = true;
        if (_currentGameObject == null) _currentGameObject = Instantiate(_objectToBuild);
        _currentGameObject.SetActive(true);
        if (_buildChecker == null) AddCollisionChecker(_currentGameObject);
        _buildChecker = _currentGameObject.GetComponent<BuildingCollisionChecker>();
        _buildChecker.BuildingsLayerMask= _buildingsLayerMask;
    }

    private void AddCollisionChecker(GameObject gameObject)
    {
        if(gameObject.GetComponent<BuildingCollisionChecker>() == null) 
        {
            gameObject.AddComponent<BuildingCollisionChecker>();
        }
    }

    public void Build(Vector3 newPosition)
    {
        if (_buildChecker.IsColliding) return;
        Instantiate(_objectToBuild, newPosition, Quaternion.identity);
    }

    public void FinishBuilding()
    {
        _canBuild = false;
        _currentGameObject.SetActive(false);
        Destroy(_currentGameObject);
    }

}


