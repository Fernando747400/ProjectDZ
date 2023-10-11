using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using com.LazyGames.DZ;

namespace com.LazyGames.DZ
{
   public class EnemyAnimatorController : MonoBehaviour
   {
        #region Variables
        [Header("Enemy Anim Configs")]
        [SerializeField] List<EnemyAnimConfg> animConfgs = new List<EnemyAnimConfg>();
        [SerializeField] private EnemyAnimConfg currentAnim;

        [Header("Testing")]
        public string TESTAnim;
        public bool valueTestAnim;



        /*
        [Header("Bolleans only Test Animations")]
        [SerializeField] private bool _isIdle, _isWalk, _isAttack; */

        [Header("Enemy Animator")]
        [SerializeField] private Animator _animator;

        public Action onChangeAnim;



        #endregion


        private void Start()
        {
            //SetAnim()
            
        }
        private void Update()
      {
        /* // Actualizar los booleanos en el Animator según las condiciones
         _animator.SetBool("IsIdle", _isIdle);
         _animator.SetBool("IsWalk", _isWalk);
         _animator.SetBool("IsAttack", _isAttack);
         
         if (!_isIdle && !_isWalk && !_isAttack)
         {
            _isIdle = true;
            _animator.SetBool("IsIdle", true);
         }*/
      }
     
        /*
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
        */
        public void SetAnim(string name, bool active)
        {
            Debug.Log("Entra aquí");
            EnemyAnimConfg config = null;
            foreach (var anim in animConfgs)
            {
                if(anim.nameAnim == name)
                {
                    config = anim;

                }
            }
            _animator.SetBool(config.nameParameter, active);
            currentAnim = config;
            onChangeAnim?.Invoke();
        }
      
      //Para Activar cualquier animacion hacer lo siguiente -->
      //_enemyAnimator.SetIdle/.SetWalk/.SetAttack/.SetFutureAnimations(true) o false para apagar
      //Idle se activara de forma automatica si no hay nada activado
   }
}
[Serializable]
public class EnemyAnimConfg
{
    public string nameAnim;
    public string nameParameter;
    public bool isActive;
    
    public void DoCrossfadeToAnim(string nextAnim)
    {

    }

}


 #if UNITY_EDITOR_WIN

 [CustomEditor(typeof(EnemyAnimatorController))]
 public class EnemyAnimatorControllerEditor : Editor
{ 
     public override void OnInspectorGUI()
    {
         DrawDefaultInspector();
        EnemyAnimatorController controller = (EnemyAnimatorController)target;


                if (GUILayout.Button("PlayAnim"))
                {
            controller.SetAnim(controller.TESTAnim, controller.valueTestAnim);
                }

    }
}
#endif