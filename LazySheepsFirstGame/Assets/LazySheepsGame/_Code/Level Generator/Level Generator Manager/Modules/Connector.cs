using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames
{
    public class Connector : MonoBehaviour
    { 
        [SerializeField] private ModuleDirection moduleDirection;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material[] materials;
        
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
        
        
        private void Start()
        {
            if (meshRenderer == null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }
            
        }

        private void OnEnable()
        {
            _isConnected = false;
        }

        private void Connect(bool value)
        {
            if (value)
            {
                //Green Material
                meshRenderer.material = materials[0];
            }
            else
            {
                //Red Material
                meshRenderer.material = materials[1];
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