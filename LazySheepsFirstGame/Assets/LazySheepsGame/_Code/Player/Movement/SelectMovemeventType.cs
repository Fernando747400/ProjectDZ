using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectMovemeventType : MonoBehaviour
{
    [SerializeField] private ContinuousMoveProviderBase _SmoothMoveCode; // Asigna el GameObject con el primer código
    [SerializeField] private TeleportationProvider _telepotMoveCode; // Asigna el GameObject con el segundo código
    [SerializeField] private XRRayInteractor _lineRendererRight;
    [SerializeField] private XRRayInteractor _lineRendererLeft;
    [SerializeField] private InputActionReference cambioCodigoAction; // Referencia al botón del Input System

    private bool codigo1Activo = true; // Inicialmente, el primer código está activo

    private void Start()
    {
        _SmoothMoveCode.enabled = true;
        _telepotMoveCode.enabled = false;
        _lineRendererRight.enabled = false;
        _lineRendererLeft.enabled = false;

        // Configura el callback para el evento de cambio de código
        cambioCodigoAction.action.performed += _ => CambiarCodigoEstado();
    }

    private void OnEnable()
    {
        // Habilita la acción cuando el objeto está activo
        cambioCodigoAction.action.Enable();
    }

    private void OnDisable()
    {
        // Deshabilita la acción cuando el objeto está desactivado
        cambioCodigoAction.action.Disable();
    }

    public void CambiarCodigoEstado()
    {
        if (codigo1Activo)
        {
            _SmoothMoveCode.enabled = false;
            _telepotMoveCode.enabled = true;
            _lineRendererRight.enabled = true;
            _lineRendererLeft.enabled = true;
        }
        else
        {
            _SmoothMoveCode.enabled = true;
            _telepotMoveCode.enabled = false;
            _lineRendererRight.enabled = false;
            _lineRendererLeft.enabled = false;
        }

        codigo1Activo = !codigo1Activo;
    }
}
