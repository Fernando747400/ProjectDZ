using UnityEngine;
using NaughtyAttributes;
using com.LazyGames.DZ;

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
