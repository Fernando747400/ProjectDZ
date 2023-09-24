using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
using Lean.Pool;
using UnityEditor;

namespace com.LazyGames
{
    public class EnemyCoreController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Enemy Core")]
        [SerializeField] private EnemyCoreData enemyCoreData;
        [SerializeField] private Transform[] spawnPoints;
        
        TimersBase _lifeTimer;
        TimersBase _waveTimer;
        
        

        #endregion

        #region private variables

        private int _currentHealth;

        #endregion

        #region unity methods

        void Start()
        {
           Initialized();
        }
        
        #endregion

        #region public methods

        public void Initialized()
        {
            _currentHealth = enemyCoreData.MaxHealth;
            SetTimers();
            
            
        }
        public void ReceiveDamage(int damage)
        {
            if(_currentHealth <= 0) return;
            _currentHealth -= damage;
            
            Debug.Log("Receive damage Current Health = ".SetColor("#F73B46") + _currentHealth);
            if (_currentHealth <= 0)
            {
                Debug.Log("Enemy Core Destroyed");
            }
        }
        
        #endregion

        #region private methods

        private void SetTimers()
        {
            Debug.Log("Set Timers");
            //Life Timer
            _lifeTimer = gameObject.AddComponent<TimersBase>();
            _lifeTimer.OnTimerEnd += () =>
            {
                Debug.Log("Life Timer End");
            };
            _lifeTimer.StartTimer(enemyCoreData.TimerLifeCoreSec, "Life Timer");
            
            //Wave Delay Timer
            _waveTimer = gameObject.AddComponent<TimersBase>();
            _waveTimer.OnTimerEnd += () =>
            {
                Debug.Log("Wave Delay Timer End");
                SpawnEnemyWave();
                _waveTimer.StartTimer(enemyCoreData.EnemySpawnDelay, "Enemy Spawn Delay Timer");
            };
            
        }
        private void SpawnEnemyWave()
        {
            var enemy = LeanPool.Spawn(enemyCoreData.EnemyPrefab);
            enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }
        
        private void StartCountDown()
        {
            
        }
        
        #endregion

    }
}

#if UNITY_EDITOR_WIN

[CustomEditor(typeof(EnemyCoreController))]
public class EnemyCoreControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnemyCoreController enemyCoreController = (EnemyCoreController) target;
        if (GUILayout.Button("Initialize Core"))
        {
            enemyCoreController.Initialized();
        }
        
        if (GUILayout.Button("Receive Damage"))
        {
            
            enemyCoreController.ReceiveDamage(100);
        }
    }
}
#endif
