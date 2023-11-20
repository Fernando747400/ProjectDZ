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
        [Header("Health")]
        [SerializeField] private int barrierHealth = 75;
        
        [Header("Enemy Wave")]
        [SerializeField] private int enemyPerWave = 10;
        [SerializeField] private GameObject enemyPrefab;
        
        [Header("Timers Wave")]
        [SerializeField] private float waveDelay = 5f;
        [SerializeField] private float enemySpawnDelay = 0.5f;
        
        [Header("Timer Life Core")]
        [SerializeField] private float timerLifeCoreSec = 30f;

        #endregion
        
        #region public variables
        public GameObject EnemyPrefab => enemyPrefab; 
        public float WaveDelay => waveDelay;
        public int EnemyPerWave => enemyPerWave;
        public float EnemySpawnDelay => enemySpawnDelay;
        public int BarrierHealth => barrierHealth;
        public float TimerLifeCoreSec => timerLifeCoreSec;
        #endregion
    }
}