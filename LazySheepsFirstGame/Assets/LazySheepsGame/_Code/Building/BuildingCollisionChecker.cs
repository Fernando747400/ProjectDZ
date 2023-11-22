using UnityEngine;
using System.Collections.Generic;
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
    [HideInInspector] public Material CooldownMaterial;

    private LayerMask _buildingsLayerMask;

    private List<MeshRenderer> _myMeshRenderers = new List<MeshRenderer>();
    private List<Material[]> _initialMaterials = new List<Material[]>(); 

    private bool _isColliding = false;
    private bool _onCooldown = false;
    private BoxCollider _boxCollider;

    public bool IsColliding { get { return _isColliding; } }
    public bool OnCooldown { get { return _onCooldown; } set { _onCooldown = value; } }
    public LayerMask BuildingsLayerMask { set { _buildingsLayerMask = value; } }
    

    public void PlaceObjectSequence()
    {
        RestoreMaterials();
        Destroy(this.GetComponent<BuildingCollisionChecker>());
    }


    private void Start()
    {
        _boxCollider = this.gameObject.GetComponent<BoxCollider>();
        CheckRendererDependencies();
    }

    private void Update()
    {
        if (_boxCollider == null)
        {
            _boxCollider = this.GetComponent<BoxCollider>();
        }
        Vector3 boxSize = _boxCollider.size;
        Vector3 center = transform.TransformPoint(_boxCollider.center);

        Collider[] colliders = Physics.OverlapBox(center, boxSize / 2f, transform.rotation, _buildingsLayerMask);

        bool hasOtherColliders = colliders.Any(collider => collider != _boxCollider);

        if (hasOtherColliders)
        {
            _isColliding = true;
            ChangeMaterials(InvalidPlacementMaterial);
        }
        else if (!_onCooldown)
        {
            _isColliding = false;
            ChangeMaterials(ValidPlacementMaterial);
        } else
        {
            _isColliding = false;
            ChangeMaterials(CooldownMaterial);
        }
    }

    private void OnCollisionEnter(Collision collision) //TODO change hammer to iBuilder
    {
        if (collision.gameObject.tag == "Hammer" || collision.gameObject.GetComponent<IBuilder>() != null)
        {
            HammerCollisionEvent.RaiseEvent();
            Debug.Log("Collision with hammer");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer" || other.gameObject.GetComponent<IBuilder>() != null)
        {
            HammerCollisionEvent.RaiseEvent();
            Debug.Log("Trigger with hammer");
        }
    }

    private void ChangeMaterials(Material materialToChange)
    {
        foreach (MeshRenderer renderer in _myMeshRenderers)
        {
            Material[] newMaterials = new Material[renderer.materials.Length];

            for (int i = 0; i < renderer.materials.Length; i++)
            {
                newMaterials[i] = materialToChange;
            }

            renderer.materials = newMaterials;
        }
    }

    private void GetMeshRenderers()
    {
        foreach(MeshRenderer renderer in this.gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            _myMeshRenderers.Add(renderer);
        }
    }

    private void GetMaterials()
    {
        foreach (MeshRenderer renderer in _myMeshRenderers)
        {
            _initialMaterials.Add(renderer.materials);
        }
    }

    private void RestoreMaterials()
    {
        for (int i = 0; i < _myMeshRenderers.Count; i++)
        {
            _myMeshRenderers[i].materials = _initialMaterials[i];
        }
    }

    private void CheckRendererDependencies()
    {
        do
        {
            GetMeshRenderers();
        } while (_myMeshRenderers == null || _myMeshRenderers.Count == 0);

        GetMaterials();
    }
}

