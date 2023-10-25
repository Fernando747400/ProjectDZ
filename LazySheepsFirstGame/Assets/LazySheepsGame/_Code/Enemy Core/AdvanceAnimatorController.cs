using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using com.LazyGames.DZ;

namespace com.LazyGames.DZ
{
   public class AdvanceAnimatorController : MonoBehaviour
   {
        #region Variables
        
        [Header("Animator")]
        [SerializeField] private Animator _animator;
        [SerializeField] private float normalizedTransitionTime = 0.3f;

        
        [Header("Anim Configs")]
        [SerializeField] List<AnimConfg> animConfgs = new List<AnimConfg>();

        [Header("Testing")]
        public string TESTAnim;
        [HideInInspector] public bool valueTestAnim = true;
        [HideInInspector] public AnimConfg previousAnim;
        
        public Action onChangeAnim;

        private AnimConfg _currentAnim;

        #endregion


        private void Start()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            _currentAnim = animConfgs[0];
        }
      
        public void SetAnim(string name)
        {
            previousAnim = _currentAnim;
            AnimConfg config = animConfgs.Find(x=>x.nameAnim == name);
            
            if(config == null)
            {
                Debug.LogError($"Anim { name } not found from = " + gameObject.name);
                return;
            }
           
            _animator.CrossFade(config.nameAnim, normalizedTransitionTime);
            _currentAnim = config;
            onChangeAnim?.Invoke();
        }
      
   }
}
[Serializable]
public class AnimConfg
{
    public string nameAnim;
    // public string nameParameter;
    // public bool isActive;
    
}


 #if UNITY_EDITOR_WIN

 [CustomEditor(typeof(AdvanceAnimatorController))]
 public class AdvanceAnimatorControllerEditor : Editor
{ 
     public override void OnInspectorGUI()
    {
         DrawDefaultInspector();
         AdvanceAnimatorController controller = (AdvanceAnimatorController)target;
         
        if (GUILayout.Button("PlayAnim")) 
        { 
            controller.SetAnim(controller.TESTAnim);
        }
        if (GUILayout.Button("PlayPreviousAnim")) 
        { 
            controller.SetAnim(controller.previousAnim.nameAnim);
        }
        

    }
}
#endif