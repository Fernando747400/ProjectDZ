using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectMovemeventType : MonoBehaviour
{
    [SerializeField] private ContinuousMoveProviderBase _SmoothMoveCode;
    [SerializeField] private SnapTurnProviderBase _SnapTurn;
    [SerializeField] private TeleportationProvider _telepotMoveCode;
    [SerializeField] private XRRayInteractor _lineRendererRight;
    [SerializeField] private XRRayInteractor _lineRendererLeft;

    [SerializeField] private InputActionReference _changeMoveTipe;
    [SerializeField] private InputActionReference _rightHandRayEnable;
    [SerializeField] private InputActionReference _leftHandRayEnable;

    private bool codigo1Activo = false;

    private void Start()
    {
        _SmoothMoveCode.enabled = true;
        _SnapTurn.enabled = true;
        _telepotMoveCode.enabled = false;
        _lineRendererRight.enabled = false;
        _lineRendererLeft.enabled = false;
        _changeMoveTipe.action.performed += _ => CambiarCodigoEstado();
        _rightHandRayEnable.action.performed += _ => CanMoveRight();
        _rightHandRayEnable.action.canceled += _ => CantMoveRight();
        _leftHandRayEnable.action.performed += _ => CanMoveLeft();
        _leftHandRayEnable.action.canceled += _ => CantMoveLeft();
    }

    private void OnEnable()
    {
        _changeMoveTipe.action.Enable();
        _rightHandRayEnable.action.Enable();
        _leftHandRayEnable.action.Enable();
    }

    private void OnDisable()
    {
        _changeMoveTipe.action.Disable();
        _rightHandRayEnable.action.Disable();
        _leftHandRayEnable.action.Disable();
    }

    public void CambiarCodigoEstado()
    {
        if(codigo1Activo)
        {
            _SmoothMoveCode.enabled = true;
            _SnapTurn.enabled = true;
            _telepotMoveCode.enabled = false;
        }
        if(!codigo1Activo)
        {
            _SmoothMoveCode.enabled = false;
            _SnapTurn.enabled = false;
            _telepotMoveCode.enabled = true;
        }

        codigo1Activo = !codigo1Activo;
    }

    private void CanMoveRight()
    {
        if (codigo1Activo)
        {
            _lineRendererRight.enabled = true;
            GameObject Retyicle = GameObject.FindGameObjectWithTag("ReticleRight");
            if(Retyicle != null ) Retyicle.SetActive(true);
        }
    }

    private void CantMoveRight() 
    {
        if (codigo1Activo)
        {
            _lineRendererRight.enabled = false;
            GameObject Retyicle = GameObject.FindGameObjectWithTag("ReticleRight");
            if (Retyicle != null) Retyicle.SetActive(false);
        }
    }

    private void CanMoveLeft()
    {
        if (codigo1Activo)
        {
            _lineRendererLeft.enabled = true;
            GameObject Retyicle = GameObject.FindGameObjectWithTag("ReticleLeft");
            if (Retyicle != null) Retyicle.SetActive(true);
        }
    }

    private void CantMoveLeft()
    {
        if (codigo1Activo)
        {
            _lineRendererLeft.enabled = false;
            GameObject Retyicle = GameObject.FindGameObjectWithTag("ReticleLeft");
            if (Retyicle != null) Retyicle.SetActive(false);
        }
    }
}