using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.DZ;
using Lean.Pool;
using UnityEngine;

public class DoomyTarget : MonoBehaviour, IGeneralTarget
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
    {
        Debug.Log("received aggressor");
        SetParticleEffect(direction);
        // _rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    private void SetParticleEffect(Vector3 pos)
    {
        GameObject particle = PoolManager.Instance.SpawnPool(PoolKeys.HITMETAL_PARTICLE_POOLKEY);
        particle.transform.position = pos;
        StartCoroutine(DespawnParticle(particle));
    }
    
    private IEnumerator DespawnParticle(GameObject particle)
    {
        yield return new WaitForSeconds(1f);
        LeanPool.Despawn(particle);
    }
}
  

