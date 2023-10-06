using UnityEngine;
using com.LazyGames.Dio;
using Lean.Pool;
using UnityEditor;
using UnityEngine.Serialization;

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
        [SerializeField] private GameObject ringVisual;
        [SerializeField] private ParticleSystem explosionParticle;
        
        [Header("UI")]
        [SerializeField] private EnemyCoreUI enemyCoreUI;

        [Header("Deactivator")] 
        [SerializeField] private VoidEventChannelSO onCoreDestroyed;
        [SerializeField] private VoidEventChannelSO onDeactivatorEnter;
        #endregion

        #region private variables

        private DeactivatorCore _deactivatorCore;
        private TimerBase _lifeTimer;
        private TimerBase _waveTimer;
        private bool _deactivatorEnter;
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
                if(_deactivatorEnter) return; 
                _deactivatorCore = other.GetComponent<DeactivatorCore>();
                // deactivatorCore.GrabInteractable.enabled = false;
                _deactivatorCore.OnDeactivatorHealthChanged += (health) =>
                {
                    enemyCoreUI.UpdateDeactivatorLifeText(health);
                };
                _deactivatorCore.OnDeactivatorDestroyed += () =>
                {
                    _lifeTimer.PauseTimer();
                    _waveTimer.PauseTimer();
                };
                SetTimers();
                _deactivatorEnter = true;
                onDeactivatorEnter.RaiseEvent();
                
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
            ringVisual.SetActive(false);
            _waveTimer.PauseTimer();
            collider.enabled = false;
            onCoreDestroyed.RaiseEvent();
            explosionParticle.Play();
            // Debug.Log("Enemy Core Destroyed".SetColor("#FE0D4F"));
        }
        private void CheckEnemyCoreState()
        {
            switch (EnemyCoreState)
            {
                case EnemyCoreState.Idle:
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
