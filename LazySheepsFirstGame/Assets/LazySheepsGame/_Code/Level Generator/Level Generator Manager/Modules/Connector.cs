using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames
{
    public class Connector : MonoBehaviour
    { 
        [SerializeField] private ModuleDirection moduleDirection;
        
        private bool _isConnected;
        
        public ModuleDirection ModuleDirection => moduleDirection;
        public bool IsConnected {
            get { return _isConnected; }
            set 
            { 
                Connect(_isConnected); 
                _isConnected = value;
            }
          
        }
        
        private MeshRenderer meshRenderer;
        
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            Connect(false);
        }
        
        
        private void Connect(bool value)
        {
            if (value)
            {
                meshRenderer.material.color = Color.green;
            }
            else
            {
                meshRenderer.material.color = Color.red;
            }
            
        }
        
    }
}

public enum ModuleDirection
{
    None,
    In,
    Out
}