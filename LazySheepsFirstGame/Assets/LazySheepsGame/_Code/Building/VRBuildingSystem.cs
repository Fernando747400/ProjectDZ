using com.LazyGames.Dio;
using UnityEngine;
using NaughtyAttributes;

public class VRBuildingSystem : MonoBehaviour
{
    [Header("Dependenices")]

    [Required]
    [SerializeField] private BuildingSystem _buildingSystem;
    [Required]
    [SerializeField] private GameObject _xrPlayerHead;

    [Header("Scriptable Objects")]
    [Required]
    [SerializeField] private BoolEventChannelSO _hammerInHandChannel;
    [Required]
    [SerializeField] private BoolEventChannelSO _xrConfirmationChannel;

    [Header("Settings")]
    [SerializeField] private float _headTiltDegrees = 20.0f;

    [SerializeField]
    private bool _hammerInHand = false; //serialized for testing only
    private bool _isBuilding = false;

    private void OnEnable()
    {
        _hammerInHandChannel.BoolEvent += UpdateHammerInHand;
    }

    private void OnDisable()
    {
        _hammerInHandChannel.BoolEvent -= UpdateHammerInHand;
    }

    private void Start()
    {
        DroppedHammer();
    }

    private void Update()
    {
        CheckForBuilding();
    }

    private void CheckForBuilding()
    {
        if (!_hammerInHand) return;

        if (_xrPlayerHead.transform.rotation.eulerAngles.x > _headTiltDegrees)
        {
            if (_isBuilding) return;
            _xrConfirmationChannel.BoolEvent(true);
            _buildingSystem.StartBuilding();
            _isBuilding = true;
        }
    }

    private void DroppedHammer()
    {
        _xrConfirmationChannel.BoolEvent(false);
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
