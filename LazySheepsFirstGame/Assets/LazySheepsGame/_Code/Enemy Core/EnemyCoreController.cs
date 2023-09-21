using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
using Lean.Pool;

public class EnemyCoreController : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private EnemyCoreData enemyCoreData;
    [SerializeField] private Transform[] spawnPoints;

    #endregion

    #region private variables

    private int _currentHealth;



    #endregion

    #region unity methods



    void Start()
    {
        _currentHealth = enemyCoreData.MaxHealth;
        SpawnEnemyWave();
    }

    void Update()
    {

    }

    #endregion

    #region private methods



    private void ReceiveDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Debug.Log("Enemy Core Destroyed");
        }
    }

    private void SpawnEnemyWave()
    {
        var enemy = LeanPool.Spawn(enemyCoreData.EnemyPrefab);
        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        
    }

#endregion

}



