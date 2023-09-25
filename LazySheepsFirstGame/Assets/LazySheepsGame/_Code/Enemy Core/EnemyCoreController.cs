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
        [SerializeField] private EnemyCoreState enemyCoreState = EnemyCoreState.None;
        [SerializeField] private Collider enemyCoreCollider;
        [SerializeField] private GameObject coreVisual;
        [SerializeField] private Transform[] spawnPoints;
        
        [Header("UI")]
        [SerializeField] private EnemyCoreUI enemyCoreUI;
        
        [Header("Deactivator")]
        [SerializeField] private DeactivatorCore deactivatorCore;
        
        TimerBase _lifeTimer;
        TimerBase _waveTimer;
        
        #endregion

        #region private variables

        private EnemyCoreState EnemyCoreState
        {
            get => enemyCoreState;
            set
            {
                enemyCoreState = value;
                CheckEnemyCoreState();
            }
        }
        
        #endregion

        #region unity methods

        private void Start()
        {
           Initialized();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<DeactivatorCore>())
            {
                deactivatorCore = other.GetComponent<DeactivatorCore>();
                
                Debug.Log("Deactivator Core Enter Enemy Core".SetColor("#FE0D4F"));
                deactivatorCore.OnDeactivatorHealthChanged += (health) =>
                {
                    enemyCoreUI.UpdateDeactivatorLifeText(health);
                };
                deactivatorCore.OnDeactivatorDestroyed += () =>
                {
                    _lifeTimer.PauseTimer();
                    _waveTimer.PauseTimer();
                };
                SetTimers();
                if(_waveTimer != null) 
                
                enemyCoreCollider.enabled = false;
            }
            
        }
        
        #endregion

        #region public methods

       
      
        
        #endregion

        #region private methods
        private void Initialized()
        {
            EnemyCoreState = EnemyCoreState.Idle;
            enemyCoreUI.SetMaxValue(enemyCoreData.TimerLifeCoreSec);
            // SetTimers();
        }

        private void SetTimers()
        {
            //Life Timer
            _lifeTimer = gameObject.AddComponent<TimerBase>();
            _lifeTimer.OnTimerEnd += () =>
            {
                if (deactivatorCore != null && deactivatorCore.CurrentHealth > 0)
                {
                    _lifeTimer.PauseTimer();
                    DestroyEnemyCore();
                }
            };
            _lifeTimer.OnTimerUpdate += (time) =>
            {
                enemyCoreUI.UpdateLifeTime(time);
            };
            _lifeTimer.StartTimer(enemyCoreData.TimerLifeCoreSec,true,0.2f ,"Life Timer");
            
            
            //Wave Delay Timer Loop
            _waveTimer = gameObject.AddComponent<TimerBase>();
            _waveTimer.OnTimerLoop += () =>
            {
                if (deactivatorCore.CurrentHealth <= 0) return;
                SpawnEnemyWave();
                EnemyCoreState = EnemyCoreState.WaveDelay;
            };
            _waveTimer.SetLoopableTimer(enemyCoreData.WaveDelay,true,0,"Wave Delay Timer");
            
           
            
        }
        private void SpawnEnemyWave()
        {
            var enemy = LeanPool.Spawn(enemyCoreData.EnemyPrefab);
            enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }
        
        private void DestroyEnemyCore()
        {
            EnemyCoreState = EnemyCoreState.Destroyed;
            coreVisual.SetActive(false);
            Debug.Log("Enemy Core Destroyed".SetColor("#FE0D4F"));
        }
        private void CheckEnemyCoreState()
        {
            switch (EnemyCoreState)
            {
                case EnemyCoreState.Idle:
                    enemyCoreUI.EnableLifeTimeUI(true);
                    break;
                case EnemyCoreState.WaveDelay:
                    break;
                
                case EnemyCoreState.Destroyed:
                    enemyCoreUI.EnableLifeTimeUI(false);
                    break;
            }
        }
        
        
        
        //Deactivator Core
      
        #endregion

    }
    
  public enum EnemyCoreState
  {
      None,
      Idle,
      WaveDelay,
      Destroyed
      
  }
    

}


// #if UNITY_EDITOR_WIN
//
// [CustomEditor(typeof(EnemyCoreController))]
// public class EnemyCoreControllerEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         EnemyCoreController enemyCoreController = (EnemyCoreController) target;
//         
//     }
// }
// #endif
