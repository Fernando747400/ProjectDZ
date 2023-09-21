using UnityEngine;
using System.Linq;

public class BuildingCollisionChecker : MonoBehaviour
{
    public bool IsColliding { get { return _isColliding; } }
    public LayerMask BuildingsLayerMask { set { _buildingsLayerMask = value; } }
    
    private LayerMask _buildingsLayerMask;

    private MeshRenderer _myMeshRenderer;
    private Color _initialColor;

    private bool _isColliding = false;
    private BoxCollider _boxCollider;

    public void PlaceObjectSequence()
    {
        this.GetComponent<MeshRenderer>().material.color = _initialColor;
        Destroy(this.GetComponent<BuildingCollisionChecker>());
    }

    private void OnEnable()
    {
        _myMeshRenderer = this.GetComponent<MeshRenderer>();
        _boxCollider = this.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _initialColor = _myMeshRenderer.material.color;
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


    private void InvalidPlacement()
    {
        _isColliding = true;
        _myMeshRenderer.material.color = Color.red;
    }

    private void ValidPlacement()
    {
        _isColliding = false;
        _myMeshRenderer.material.color = Color.green;
    }
}

