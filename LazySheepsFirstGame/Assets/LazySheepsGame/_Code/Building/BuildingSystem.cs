using com.LazyGames.Dio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private GameObject _groundGameObject;

    [SerializeField] private BoolEventChannelSO _collisionChannelSO;

    private bool _canBuild = false;
    private bool _isColliding = false;

    private void OnEnable()
    {
        _collisionChannelSO.BoolEvent += UpdateCollision;
        _groundGameObject = Instantiate(_groundGameObject);
    }

    private void OnDisable()
    {
        _collisionChannelSO.BoolEvent -= UpdateCollision;
    }

    private void Update()
    {
        if (!_canBuild) return;

        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(raycast, out RaycastHit hit, 1000, _groundLayerMask)) 
        {
            _groundGameObject.transform.position = hit.point;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (_isColliding) return;
            GameObject go = Instantiate(_groundGameObject, hit.point, Quaternion.identity);
            go.GetComponent<BuildingCollisionChecker>().Destroy();
        } else if (Input.GetMouseButtonUp(1)) {
            _canBuild = false;
            _groundGameObject.SetActive(false);
        }
    }
    public void StartBuilding()
    {
        _canBuild = true;
        _groundGameObject.SetActive(true);
        AddCollisionChecker(_groundGameObject);
    }

    private void UpdateCollision(bool onCollision)
    {
        _isColliding = onCollision;
        UpdateColor(onCollision);
    }

    private void UpdateColor(bool onCollision)
    {
        if (onCollision) _groundGameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        if (!onCollision) _groundGameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void AddCollisionChecker(GameObject gameObject)
    {
        if(gameObject.GetComponent<BuildingCollisionChecker>() == null) 
        {
            gameObject.AddComponent<BuildingCollisionChecker>();
            gameObject.GetComponent<BuildingCollisionChecker>().CollisionChannelSO = _collisionChannelSO;
        }
    }

}


