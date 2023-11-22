using System;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames.Dio;
using Lean.Pool;
using UnityEditor;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace com.LazyGames
{
    public class EnemyCoreController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Enemy Core")]
        [SerializeField] private EnemyCoreData enemyCoreData;
        [SerializeField] private EnemyCoreState enemyCoreState = EnemyCoreState.None;
        [SerializeField] private Collider collider;
        [SerializeField] private GameObject coreVisual;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private GameObject barrierVisual;
        
        [Header("UI")]
        [SerializeField] private EnemyCoreUI enemyCoreUI;

        [Header("Deactivator")] 
        [SerializeField] private VoidEventChannelSO onCoreDestroyed;
        [SerializeField] private VoidEventChannelSO onDeactivatorEnterCore;
        [SerializeField] private GameObjectEventChannelSO DeactivatorSender;
        #endregion

        #region private variables

        private DeactivatorCore _deactivatorCore;
        private TimerBase _lifeTimer;
        private TimerBase _waveTimer;
        // private bool _deactivatorEnter;
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

        #region public variables
        public EnemyCoreData EnemyCoreData => enemyCoreData;

        #endregion
        
        #region unity methods

        private void Start()
        {
           Initialized();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                SpawnEnemyWave();
            }
        }

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.GetComponent<DeactivatorCore>())
        //     {
        //         if(_deactivatorEnter) return; 
        //         _deactivatorCore = other.GetComponent<DeactivatorCore>();
        //         // deactivatorCore.GrabInteractable.enabled = false;
        //        
        //         
        //         _deactivatorCore.OnDeactivatorHealthChanged += (health) =>
        //         {
        //             enemyCoreUI.UpdateDeactivatorLifeText(health);
        //         };
        //         _deactivatorCore.OnDeactivatorDestroyed += () =>
        //         {
        //             _lifeTimer.PauseTimer();
        //             _waveTimer.PauseTimer();
        //         };
        //         SetTimers();
        //         _deactivatorEnter = true;
        //         onDeactivatorEnter.RaiseEvent();
        //         
        //     }
        //     
        // }
        
        #endregion

        #region public methods
        
        public void BarrierDestroyed()
        {
            barrierVisual.SetActive(false);
            collider.enabled = false;
        }
       
      
        
        #endregion

        #region private methods
        private void Initialized()
        {
            EnemyCoreState = EnemyCoreState.BlockedCore;
            enemyCoreUI.SetMaxValue(enemyCoreData.TimerLifeCoreSec);
            DeactivatorSender.GameObjectEvent += RecieveDeactivatorGO;
            onDeactivatorEnterCore.VoidEvent += OnDeactivatorEnter;
            
        }

        private void RecieveDeactivatorGO(GameObject deeactivator)
        {
            _deactivatorCore = deeactivator.GetComponent<DeactivatorCore>();
        }
        private void OnDeactivatorEnter()
        {
            SetTimers();
            _deactivatorCore.OnDeactivatorHealthChanged += (health) =>
            {
                enemyCoreUI.UpdateDeactivatorLifeText(health);
            };
            _deactivatorCore.OnDeactivatorDestroyed += () =>
            {
                _lifeTimer.PauseTimer();
                _waveTimer.PauseTimer();
            };
            
        }
        
        private void SetTimers()
        {
            //Life Timer
            _lifeTimer = this.gameObject.AddComponent<TimerBase>();
            _lifeTimer.OnTimerEnd += () =>
            {
                if (_deactivatorCore != null && _deactivatorCore.CurrentHealth > 0)
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
                if (_deactivatorCore.CurrentHealth <= 0) return;
                SpawnEnemyWave();
            };
            _waveTimer.SetLoopableTimer(enemyCoreData.WaveDelay,true,0,"Wave Delay Timer");
            EnemyCoreState = EnemyCoreState.WaveDelay;
            
            // Debug.Log("SetTimers".SetColor("#FE0D4F"));
            
        }
        private void SpawnEnemyWave()
        {
            // // foreach (var placesToSpawnPoint in spawnPoints)
            // // {
            // //     var enemy = LeanPool.Spawn(enemyCoreData.EnemyPrefab); 
            // //     enemy.transform.position = placesToSpawnPoint.position;
            // // }
            //
            // for (int i = 0; i < spawnPoints.Length; i++)
            // {
            //     var enemy = LeanPool.Spawn(enemyCoreData.EnemyPrefab); 
            //     enemy.transform.position = spawnPoints[i].position;
            //     
            // }
            
          var enemy = LeanPool.Spawn(enemyCoreData.EnemyPrefab); 
          int randomPlace = Random.Range(0, spawnPoints.Length); 
          enemy.transform.position = spawnPoints[randomPlace].position;  
        }
        
        private void DestroyEnemyCore()
        {
            EnemyCoreState = EnemyCoreState.Destroyed;
            coreVisual.SetActive(false);
            _waveTimer.PauseTimer();
            collider.enabled = false;
            onCoreDestroyed.RaiseEvent();
            explosionParticle.Play();
            // Debug.Log("Enemy Core Destroyed".SetColor("#FE0D4F"));
        }
        private void CheckEnemyCoreState()
        {
            // Debug.Log("EnemyCoreState = ".SetColor("#FE0D4F") + EnemyCoreState);
            switch (EnemyCoreState)
            {
                case EnemyCoreState.BlockedCore:
                    // enemyCoreUI.EnableLifeTimeUI(false);
                    break;
                case EnemyCoreState.WaveDelay:
                    enemyCoreUI.EnableLifeTimeUI(true);
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
      BlockedCore,
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
