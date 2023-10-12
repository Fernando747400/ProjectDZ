using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using com.LazyGames;
using com.LazyGames.DZ;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.LazyGames
{
    public class EnemyTarget : MonoBehaviour
    {
        #region public Methods

        [SerializeField] private AdvanceAnimatorController animatorController;
        [SerializeField] private EnemyController enemyController;
        public bool test;

        private Vector3 currentPosition;

        private Mouse _mouse;
        private void Start()
        {
            _mouse = Mouse.current;
        }

        private void Update()
        {
            if (test)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    
                    HandleHitPoint(_mouse.position.ReadValue());
                }
            }
        }

        private void HandleHitPoint(Vector3 direction)
        {
            // handle which area of the body was hit
            currentPosition = transform.position;
            Vector3 hitPointPosition = direction - currentPosition;
            float angle = Vector3.Angle(hitPointPosition, transform.forward);
            Debug.Log(angle);
            
            
        }

        private void SetBleedingEffect()
        {
            
        }
            

        #endregion
        
    }

    public enum EnemyBodyPart
    {
        Head,
        LeftSide,
        RightSide,
    }
}