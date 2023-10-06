using com.LazyGames.Dio;
using UnityEngine;

public class VRBuildingSystem : MonoBehaviour
{
    [Header("Dependenices")]
    [SerializeField] private BuildingSystem _buildingSystem;
    [SerializeField] private GameObject _vrHead;

    [Header("Scriptable Objects")]
    [SerializeField] private BoolEventChannelSO _hammerInHandChannel;

    [Header("Settings")]
    [SerializeField] private float _headTiltDegrees;

    [SerializeField] private bool _hammerInHand = false;
    private bool _isBuilding = false;

    private void OnEnable()
    {
        _hammerInHandChannel.BoolEvent += UpdateHammerInHand;
    }

    private void OnDisable()
    {
        _hammerInHandChannel.BoolEvent -= UpdateHammerInHand;
    }

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

    private void UpdateHammerInHand(bool inHand)
    {
        _hammerInHand = inHand;

        if(!_hammerInHand)
        {
            DroppedHammer();
        }
    }
}
