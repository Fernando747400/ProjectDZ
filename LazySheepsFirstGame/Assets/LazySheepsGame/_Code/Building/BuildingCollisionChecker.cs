using UnityEngine;
using System.Linq;
using com.LazyGames.Dio;

public class BuildingCollisionChecker : MonoBehaviour
{
    [Header("Dependencies")]
    [Header("Scriptable Objects")]
    [HideInInspector] public VoidEventChannelSO HammerCollisionEvent;

    [Header("Materials Dependencies")]
    [HideInInspector] public Material ValidPlacementMaterial;
    [HideInInspector] public Material InvalidPlacementMaterial;


    public bool IsColliding { get { return _isColliding; } }
    public LayerMask BuildingsLayerMask { set { _buildingsLayerMask = value; } }
    
    private LayerMask _buildingsLayerMask;

    private MeshRenderer _myMeshRenderer;
    private Material _initialMaterial; 

    private bool _isColliding = false;
    private BoxCollider _boxCollider;

    public void PlaceObjectSequence()
    {
        this.GetComponent<MeshRenderer>().material = _initialMaterial;
        Destroy(this.GetComponent<BuildingCollisionChecker>());
    }

    private void OnEnable()
    {
        _myMeshRenderer = this.GetComponent<MeshRenderer>();
        _boxCollider = this.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _initialMaterial = _myMeshRenderer.material;
    }

    private void Update()
    {
        Vector3 boxSize = _boxCollider.size;
        Vector3 center = transform.TransformPoint(_boxCollider.center);

        Collider[] colliders = Physics.OverlapBox(center, boxSize / 2f, transform.rotation, _buildingsLayerMask);

        bool hasOtherColliders = colliders.Any(collider => collider != _boxCollider);

        if (hasOtherColliders)
        {
            InvalidPlacement();
        }
        else
        {
            ValidPlacement();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            HammerCollisionEvent.RaiseEvent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer")
        {
            HammerCollisionEvent.RaiseEvent();
        }
    }


    private void InvalidPlacement()
    {
        _isColliding = true;
        _myMeshRenderer.material = InvalidPlacementMaterial;
    }

    private void ValidPlacement()
    {
        _isColliding = false;
        _myMeshRenderer.material = ValidPlacementMaterial;
    }
}

