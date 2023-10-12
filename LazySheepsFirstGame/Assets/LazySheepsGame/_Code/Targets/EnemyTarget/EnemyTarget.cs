using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using com.LazyGames;
using com.LazyGames.DZ;
using UnityEngine;

namespace com.LazyGames
{
    public class EnemyTarget : MonoBehaviour
    {
        #region public Methods

        [SerializeField] private EnemyAnimatorController animatorController;


        private void Start()
        {
            
        }

        private void HandleHitPoint(Vector3 direction)
        {
            // handle which area of the body was hit

            
            
            
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