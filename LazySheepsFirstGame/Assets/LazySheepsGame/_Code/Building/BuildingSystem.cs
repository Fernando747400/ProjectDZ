using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    [SerializeField] private LayerMask _groundLayerMask;
private bool _canBuild = false;

    public void StartBuilding()
    {
        _canBuild = true;
    }

    private void Update()
    {
        if (!_canBuild) return;

        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(raycast, out RaycastHit hit, 1000, _groundLayerMask)) 
        {

        }
    }

}


