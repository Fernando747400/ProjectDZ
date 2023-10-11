using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace com.LazyGames.DZ
{
    public class HandController : MonoBehaviour
    {
        #region Variables

        [Header("Inputs")]
        [SerializeField] private InputActionReference _gripInput;
        [SerializeField] private InputActionReference _trggerInput;
        [SerializeField] private InputActionReference _indexInput;
        [SerializeField] private InputActionReference _thumbInput;

        [Header("Animator")]
        [SerializeField] private Animator _handAnimator;

        #endregion

        private void Awake()
        {
            _handAnimator = GetComponent<Animator>();
        }
        
        
        void Update()
        {
            if (!_handAnimator) return;

            float grip = _gripInput.action.ReadValue<float>();
            float trigger = _trggerInput.action.ReadValue<float>();
            float indexTouch = _indexInput.action.ReadValue<float>();
            float thumbTouch = _thumbInput.action.ReadValue<float>();
            
            _handAnimator.SetFloat("Grip", grip);
            _handAnimator.SetFloat("Trigger", trigger);
            _handAnimator.SetFloat("Index", indexTouch);
            _handAnimator.SetFloat("Thumb", thumbTouch);
        }
    }
    
}
