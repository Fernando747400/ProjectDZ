using com.LazyGames.Dio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _buildingsLayerMask;
    [SerializeField] private GameObject _groundGameObjectReference;



    private bool _canBuild = false;
    private GameObject _currentGameObject;
    private BuildingCollisionChecker _buildChecker;


    private void Update()
    {
        if (!_canBuild) return;

        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(raycast, out RaycastHit hit, 1000, _groundLayerMask))
        {
            Vector3 objectSize = _currentGameObject.GetComponent<Renderer>().bounds.size;

            Vector3 newPosition = hit.point + Vector3.up * (objectSize.y / 2);

            _currentGameObject.transform.position = newPosition;

            if (Input.GetMouseButtonDown(0))
            {
                if (_buildChecker.IsColliding) return;
                GameObject go = Instantiate(_groundGameObjectReference, newPosition, Quaternion.identity);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                _canBuild = false;
                _currentGameObject.SetActive(false);
                Destroy(_currentGameObject);
            }
        }
    }

    public void StartBuilding()
    {
        _canBuild = true;
        if (_currentGameObject == null) _currentGameObject = Instantiate(_groundGameObjectReference);
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

}


