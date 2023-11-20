using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.DZ;
using Lean.Pool;
using UnityEngine;

public class EnemyCoreBarrierTarget : MonoBehaviour, IGeneralTarget
{
    [SerializeField] private EnemyCoreController enemyCoreController;
    [SerializeField] private int health;
    void Start()
    {
        health = enemyCoreController.EnemyCoreData.BarrierHealth;
    }
    
    
    public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
    {
        HitParticle(direction);
        health -= (int) dmg;

        if (health <= 0)
        {
            enemyCoreController.BarrierDestroyed();
        }
    }
    
    private void HitParticle(Vector3 pos)
    {
        GameObject hitParticle = PoolManager.Instance.SpawnPool(PoolKeys.HITMETAL_PARTICLE_POOLKEY);
        hitParticle.transform.position = pos;
        StartCoroutine(DespawnParticle(hitParticle));

    }
    
    private IEnumerator DespawnParticle(GameObject particle)
    {
        yield return new WaitForSeconds(1f);
        LeanPool.Despawn(particle);
    }
}
