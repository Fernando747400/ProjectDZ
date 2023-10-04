using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectMovemeventType : MonoBehaviour
{
    [SerializeField] private ContinuousMoveProviderBase _SmoothMoveCode;
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
        if (codigo1Activo)
        {
            _SmoothMoveCode.enabled = false;
            _telepotMoveCode.enabled = true;
        }
        else
        {
            _SmoothMoveCode.enabled = true;
            _telepotMoveCode.enabled = false;
        }

        codigo1Activo = !codigo1Activo;
    }

    private void CanMoveRight()
    {
        _lineRendererRight.enabled = true;
        GameObject[] DeleteRetyicle = GameObject.FindGameObjectsWithTag("Reticle");
        foreach (GameObject objeto in DeleteRetyicle)
        {
            objeto.SetActive(true);
        }
    }

    private void CantMoveRight() 
    {
        _lineRendererRight.enabled = false;
        GameObject[] DeleteRetyicle = GameObject.FindGameObjectsWithTag("Reticle");
        foreach (GameObject objeto in DeleteRetyicle)
        {
            objeto.SetActive(false);
        }
    }

    private void CanMoveLeft()
    {
        _lineRendererLeft.enabled = true;
        GameObject[] DeleteRetyicle = GameObject.FindGameObjectsWithTag("Reticle");
        foreach (GameObject objeto in DeleteRetyicle)
        {
            objeto.SetActive(true);
        }
    }

    private void CantMoveLeft()
    {
        _lineRendererLeft.enabled = false;
        GameObject[] DeleteRetyicle = GameObject.FindGameObjectsWithTag("Reticle");
        foreach (GameObject objeto in DeleteRetyicle)
        {
            objeto.SetActive(false);
        }
    }
}