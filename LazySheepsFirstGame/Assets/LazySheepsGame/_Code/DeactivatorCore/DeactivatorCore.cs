using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
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
        [SerializeField] private VoidEventChannelSO onCoreDestroyed;
        [SerializeField] private XRGrabInteractable grabInteractable;

        

        #endregion

        #region private variables

        private int _currentHealth;

        #endregion

        #region public variables

        public XRGrabInteractable GrabInteractable => grabInteractable;
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
        }
        private void OnTriggerEnter(Collider other)
        {
            // if (other.CompareTag("Enemy"))
            // {
            //     Debug.Log("Enemy Enter Deactivator Core".SetColor("#FE0D4F"));
            //     ReceiveDamage(5);
            // }
        }
       
        #endregion
        
        public void ReceiveDamage(int damage)
        {
            if(_currentHealth <= 0) return;
            _currentHealth -= damage;
            OnDeactivatorHealthChanged?.Invoke(_currentHealth);
            
            Debug.Log("Receive damage Current Health = ".SetColor("#F73B46") + _currentHealth);
            if (_currentHealth <= 0)
            {
                Debug.Log("Deactivator Destroyed".SetColor("#FE0D4F"));
                OnDeactivatorDestroyed?.Invoke();
            }
        }
        
#region private methods

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
//         if (GUILayout.Button("Receive Damage"))
//         {
//             deactivatorCore.ReceiveDamage(5);
//         }
//     }
// }
//
// #endif
