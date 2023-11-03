using UnityEngine;
using NaughtyAttributes;

public class ReferenceRaiser : MonoBehaviour
{
    [Header("Dependencies")]

    [Required]
    [SerializeField] private ReferencePool _referencePool;

    private void OnEnable()
    {
        _referencePool.AddReference(this.gameObject);
    }

    private void OnDisable()
    {
        _referencePool.RemoveReference(this.gameObject);
    }
}
