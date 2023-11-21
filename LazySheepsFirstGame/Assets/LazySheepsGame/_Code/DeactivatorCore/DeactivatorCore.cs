using System;
using System.Collections;
using com.LazyGames;
using com.LazyGames.Dio;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace com.LazyGames
{
    public class DeactivatorCore : MonoBehaviour
    {

        #region Serialized Fields

        [SerializeField] private int maxHealth = 100;
        [SerializeField] private Collider collider;

        [Header("Trigger")] 
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody triggerRigidbody;
        [SerializeField] private GameObject triggerGO;
        
        [Header("Events")]
        [SerializeField] private VoidEventChannelSO onCoreDestroyed;
        [SerializeField] private VoidEventChannelSO onDeactivatorIsPlaced;
        [SerializeField] private VoidEventChannelSO onDeactivatorEnterCore;
        [SerializeField] private GameObjectEventChannelSO DeactivatorSender;

        #endregion

        #region private variables

        private int _currentHealth;
        private bool _deactivatorIsPlaced;

    #endregion

        #region public variables
        
        public Collider Collider => collider;
        public int CurrentHealth => _currentHealth;
        public Action OnDeactivatorDestroyed;
        public Action<int> OnDeactivatorHealthChanged;
        
        #endregion

        #region unity methods
        void Start()
        {
            _currentHealth = maxHealth;
            onCoreDestroyed.VoidEvent += () => { Destroy(gameObject); };
            onDeactivatorIsPlaced.VoidEvent += StartDeactivator;
        }
        
        private void OnDestroy()
        {
            onCoreDestroyed.VoidEvent -= () => { Destroy(gameObject); };
            onDeactivatorIsPlaced.VoidEvent -= StartDeactivator;
        }
        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.CompareTag("Enemy"))
        //     {
        //         Debug.Log("Enemy Enter Deactivator Core".SetColor("#FE0D4F"));
        //         ReceiveDamage(5);
        //     }
        // }
       
        #endregion
        private void AddForceTrigger()
        {
            triggerRigidbody.useGravity = true;
            triggerRigidbody.isKinematic = false;
            
            Vector3 direction = new Vector3(triggerGO.transform.position.x -1 , triggerGO.transform.position.y + 1, triggerGO.transform.position.z - 0.5f);
            triggerRigidbody.AddForce(direction, ForceMode.Impulse);
        }
        
        public void SetTriggerAnimator()
        {
            animator.Play("GranadeCore");
            triggerRigidbody = triggerGO.AddComponent<Rigidbody>();
            // triggerGO.AddComponent<MeshCollider>();
            AddForceTrigger();
            
            StartCoroutine(EnableTrigger());
        }
        public void ReceiveDamage(int damage)
        {
            if(_currentHealth <= 0) return;
            _currentHealth -= damage;
            OnDeactivatorHealthChanged?.Invoke(_currentHealth);
            
            // Debug.Log("Receive damage Current Health = ".SetColor("#F73B46") + _currentHealth);
            if (_currentHealth <= 0)
            {
                Debug.Log("Deactivator Destroyed".SetColor("#FE0D4F"));
                OnDeactivatorDestroyed?.Invoke();
            }
        }
        
        #region private methods
        private void StartDeactivator()
        {
            _deactivatorIsPlaced = true;
            DeactivatorSender.RaiseEvent(this.gameObject);
            StartCoroutine(StartAnimationDelay());
        }

        public void MoveGranadeDown()
        {
            transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y- 0.3f, transform.localPosition.z),
                1.5f).onComplete += () =>
            {
                onDeactivatorEnterCore.RaiseEvent();
                Debug.Log("Deactivator Enter Core".SetColor("#FE0D4F"));
            };
        }
        IEnumerator EnableTrigger()
        {
            yield return new WaitForSeconds(1);
            triggerRigidbody.useGravity = false;
            triggerRigidbody.isKinematic = true;
            triggerGO.SetActive(false);
        }
        IEnumerator StartAnimationDelay()
        {
            yield return new WaitForSeconds(1);
            SetTriggerAnimator();
        }
        
        #endregion
    }
    
    
}

// #if UNITY_EDITOR_WIN
// [CustomEditor(typeof(DeactivatorCore))]
// public class DeactivatorCoreEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         DeactivatorCore deactivatorCore = (DeactivatorCore) target;
//         
//         if (GUILayout.Button("Add force on trigger"))
//         {
//             deactivatorCore.SetTriggerAnimator();
//         }
//     }
// }
//
// #endif
