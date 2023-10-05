using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectMovemeventType : MonoBehaviour
{
    [SerializeField] private ContinuousMoveProviderBase _SmoothMoveCode;
    [SerializeField] private TeleportationProvider _telepotMoveCode;
    [SerializeField] private XRRayInteractor _lineRendererRight;
    [SerializeField] private XRRayInteractor _lineRendererLeft;
    [SerializeField] private InputActionReference cambioCodigoAction;

    private bool codigo1Activo = true;

    private void Start()
    {
        _SmoothMoveCode.enabled = true;
        _telepotMoveCode.enabled = false;
        _lineRendererRight.enabled = false;
        _lineRendererLeft.enabled = false;
        cambioCodigoAction.action.performed += _ => CambiarCodigoEstado();
    }

    private void OnEnable()
    {
        cambioCodigoAction.action.Enable();
    }

    private void OnDisable()
    {
        cambioCodigoAction.action.Disable();
    }

    public void CambiarCodigoEstado()
    {
        if (codigo1Activo)
        {
            GameObject[] DeleteRetyicle = GameObject.FindGameObjectsWithTag("Reticle");
            foreach (GameObject objeto in DeleteRetyicle)
            {
                objeto.SetActive(true);
            }
            _SmoothMoveCode.enabled = false;
            _telepotMoveCode.enabled = true;
            _lineRendererRight.enabled = true;
            _lineRendererLeft.enabled = true;
        }
        else
        {
            GameObject[] DeleteRetyicle = GameObject.FindGameObjectsWithTag("Reticle");
            foreach (GameObject objeto in DeleteRetyicle)
            {
                objeto.SetActive(false);
            }
            _SmoothMoveCode.enabled = true;
            _telepotMoveCode.enabled = false;
            _lineRendererRight.enabled = false;
            _lineRendererLeft.enabled = false;
        }

        codigo1Activo = !codigo1Activo;
    }
}
