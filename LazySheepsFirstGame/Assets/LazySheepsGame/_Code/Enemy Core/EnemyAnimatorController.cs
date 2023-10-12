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
        
        [Header("Enemy Animator")]
        [SerializeField] private Animator _animator;
        
        [Header("Enemy Anim Configs")]
        [SerializeField] List<EnemyAnimConfg> animConfgs = new List<EnemyAnimConfg>();
        [SerializeField] private EnemyAnimConfg currentAnim;
        [SerializeField] private float normalizedTransitionTime = 0.3f;

        [Header("Testing")]
        public string TESTAnim;
        public bool valueTestAnim;
        [HideInInspector] public EnemyAnimConfg previousAnim;
      

        public Action onChangeAnim;



        #endregion


        private void Start()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            currentAnim = animConfgs[0];
        }
      
        public void SetAnim(string name, bool active)
        {
            previousAnim = currentAnim;
            EnemyAnimConfg config = null;
            foreach (var anim in animConfgs)
            {
                if(anim.nameAnim == name)
                {
                    config = anim;

                }
            }
            // _animator.SetBool(config.nameParameter, active);
            _animator.CrossFade(config.nameAnim, normalizedTransitionTime);
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
        if (GUILayout.Button("PlayPreviousAnim")) 
        { 
            controller.SetAnim(controller.previousAnim.nameAnim, controller.previousAnim.isActive);
        }
        

    }
}
#endif