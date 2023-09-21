using UnityEngine;
using com.LazyGames.Dio;

public class BuildingCollisionChecker : MonoBehaviour
{
    public BoolEventChannelSO CollisionChannelSO {  get { return _collisionChannelSO; }  set { _collisionChannelSO = value; } }
    
    [SerializeField] private BoolEventChannelSO _collisionChannelSO;

    private Collider _collisionCollider;
    private Rigidbody _collisionRigidbody;

    public void Destroy()
    {
        Destroy(_collisionCollider);
        Destroy(_collisionRigidbody);
        Destroy(this);
    }

    private void OnEnable()
    {
        CheckDependencies();
    }

    private void CheckDependencies()
    {
        if (this.gameObject.GetComponent<Collider>() == null) _collisionCollider = this.gameObject.AddComponent<MeshCollider>();
        if (this.gameObject.GetComponent<Rigidbody>() == null) _collisionRigidbody = this.gameObject.AddComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionChannelSO.RaiseEvent(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisionChannelSO.RaiseEvent(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        _collisionChannelSO.RaiseEvent(false);
    }

    private void OnTriggerExit(Collider other)
    {
        _collisionChannelSO.RaiseEvent(false);
    }
}
