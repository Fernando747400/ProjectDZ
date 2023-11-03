using com.LazyGames.Dio;
using UnityEngine;
using NaughtyAttributes;

public class VRBuildingSystem : MonoBehaviour
{
    [Header("Dependenices")]

    [Required]
    [SerializeField] private BuildingSystem _buildingSystem;
    [Required]
    [SerializeField] private GameObject _vrHead;

    [Header("Scriptable Objects")]
    [SerializeField] private BoolEventChannelSO _hammerInHandChannel;

    [Header("Settings")]
    [SerializeField] private float _headTiltDegrees = 35.0f;

    private bool _hammerInHand = false;
    private bool _isBuilding = false;

    private void OnEnable()
    {
        _hammerInHandChannel.BoolEvent += UpdateHammerInHand;
        DroppedHammer();
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
            if (_isBuilding) return;
            _buildingSystem.VRConfirmation = true;
            _buildingSystem.StartBuilding();
            _isBuilding = true;
        }
    }

    private void DroppedHammer()
    {
        _buildingSystem.VRConfirmation = false;
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
