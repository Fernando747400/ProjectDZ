// Creado Raymundo Mosqueda 25/10/23

using System;
using com.LazyGames.DZ;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class NoiseObj : MonoBehaviour, INoiseSource
{
    [SerializeField] private NoiseParameters noiseParams;
    private Rigidbody _rb;
    private float _relativeSpeed;

    private bool _visualize;
    // Start is called before the first frame update
    void Start()
    {
        Prepare();
        _visualize = true;
    }

    private void Update()
    {
        // Debug.Log(_rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _relativeSpeed = collision.relativeVelocity.magnitude;
        MakeNoise(noiseParams, _rb.velocity.magnitude, transform.position);
    }

    private void Prepare()
    {
        _rb = GetComponentInChildren<Rigidbody>();
    }

    public void MakeNoise(NoiseParameters noiseParameters, float velocity, Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, noiseParameters.baseRadius * _relativeSpeed, noiseParameters.layerMask);
        if(hits.Length == 0) return;
        foreach (var col in hits)
        {
            if (!col.gameObject.TryGetComponent<INoiseSensitive>(out var noiseSensitive)) continue;
            var dist = Vector3.Distance(position, col.transform.position);
            noiseSensitive.HearNoise(noiseParams.loudness / dist, position, noiseParams.dangerous);
        }
    }

    private void OnDrawGizmos()
    {
        if (!_visualize) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, noiseParams.baseRadius * _relativeSpeed);
    }
}
