using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
[CreateAssetMenu(fileName = "EnemyCoreData", menuName = "LazySheeps/Enemy Core", order = 1)]
    public class EnemyCoreData : ScriptableObject
    {
        public int health = 1000;
        public int enemyPerWave = 10;
        public float spawnDelay = 0.5f;
        
        [SerializeField] private float waveDelay = 5f;
        [SerializeField] private GameObject enemyPrefab;

        public GameObject EnemyPrefab => enemyPrefab; 
        public float WaveDelay => waveDelay;

    }
}