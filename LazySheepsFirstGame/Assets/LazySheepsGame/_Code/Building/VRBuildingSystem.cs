using UnityEngine;

public class VRBuildingSystem : MonoBehaviour
{
    [Header("Dependenices")]
    [SerializeField] private BuildingSystem _buildingSystem;
    [SerializeField] private GameObject _vrHead;

    [Header("Settings")]
    [SerializeField] private float _headTiltDegrees;

    public bool HammerInHand { get { return _hammerInHand; } set { _hammerInHand = value; } }

    private bool _hammerInHand = true;
    private bool _isBuilding = false;

    private void Update()
    {
        CheckForBuilding();
    }

    private void CheckForBuilding()
    {
        if (!_hammerInHand)
        {
            _buildingSystem.VRConfirmation = false;
            return;
        }
        else if (_vrHead.transform.rotation.eulerAngles.x > _headTiltDegrees)
        {
            _buildingSystem.VRConfirmation = true;
            if (_isBuilding) return;
            _buildingSystem.StartBuilding();
            _isBuilding = true;
        }
    }

    private void DroppedHammer()
    {
        StopBuilding();
    }

    private void StopBuilding()
    {
        _buildingSystem.FinishBuilding();
        _isBuilding = false;
    }
}
