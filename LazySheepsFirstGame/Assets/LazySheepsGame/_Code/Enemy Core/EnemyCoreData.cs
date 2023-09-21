using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames
{
[CreateAssetMenu(fileName = "EnemyCoreData", menuName = "LazySheeps/Enemy Core", order = 1)]
    public class EnemyCoreData : ScriptableObject
    {
        #region Serialized Fields
        [SerializeField] private int maxHealth = 1000;
        [SerializeField] private int enemyPerWave = 10;
        [SerializeField] private float spawnDelay = 0.5f;
        [SerializeField] private float waveDelay = 5f;
        [SerializeField] private GameObject enemyPrefab;
        #endregion
        
        #region public variables
        public GameObject EnemyPrefab => enemyPrefab; 
        public float WaveDelay => waveDelay;
        public int EnemyPerWave => enemyPerWave;
        public float SpawnDelay => spawnDelay;
        public int MaxHealth => maxHealth;
        #endregion
    }
}