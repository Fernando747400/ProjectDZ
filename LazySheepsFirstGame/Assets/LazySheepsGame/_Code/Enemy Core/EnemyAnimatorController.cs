using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames.DZ
{
   public class EnemyAnimatorController : MonoBehaviour
   {
      #region Variables

      [Header("Bolleans only Test Animayions")]
      private bool _isIdle, _isWalk, _isAttack; 
      [Header("Enemy Animator")]
      [SerializeField] private Animator _animator;


      #endregion


      private void Update()
      {
         // Actualizar los booleanos en el Animator según las condiciones
         _animator.SetBool("IsIdle", _isIdle);
         _animator.SetBool("IsWalk", _isWalk);
         _animator.SetBool("IsAttack", _isAttack);
         
         if (!_isIdle && !_isWalk && !_isAttack)
         {
            _isIdle = true;
            _animator.SetBool("IsIdle", true);
         }
      }
      
      public void SetIdle(bool value)
      {
         _isIdle = value;
      }

      public void SetWalk(bool value)
      {
         _isWalk = value;
      }

      public void SetAttack(bool value)
      {
         _isAttack = value;
      }
      
      //Para Activar cualquier animacion hacer lo siguiente -->
      //_enemyAnimator.SetIdle/.SetWalk/.SetAttack/.SetFutureAnimations(true) o false para apagar
      //Idle se activara de forma automatica si no hay nada activado
   }
}
