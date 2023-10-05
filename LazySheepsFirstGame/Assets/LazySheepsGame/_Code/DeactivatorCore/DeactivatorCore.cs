using System;
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
        [SerializeField] private VoidEventChannelSO onDeactivatorIsPlaced;
        [SerializeField] private XRGrabInteractable grabInteractable;
        [SerializeField] private InteractionLayerMask nonInteractableLayers;



        #endregion

        #region private variables

        private int _currentHealth;
        private bool _deactivatorIsPlaced;

    #endregion

        #region public variables

        public XRGrabInteractable GrabInteractable
        {
            get => grabInteractable;
            set => grabInteractable = value;
        }
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
            onDeactivatorIsPlaced.VoidEvent += () =>
            {
                _deactivatorIsPlaced = true;
                // grabInteractable.interactionLayers = nonInteractableLayers;
                Debug.Log("Deactivator Is Placed ".SetColor("#FE0D4F") + nonInteractableLayers);
            };
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Enter Deactivator Core".SetColor("#FE0D4F"));
                ReceiveDamage(5);
            }
        }
       
        #endregion
        
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

        public void LastSelectedExit(SelectExitEventArgs arg)
        {
            if (_deactivatorIsPlaced)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                grabInteractable.interactionLayers = nonInteractableLayers;
                
            }
            Debug.Log("LastSelectedExit".SetColor("#FE0D4F"));

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
