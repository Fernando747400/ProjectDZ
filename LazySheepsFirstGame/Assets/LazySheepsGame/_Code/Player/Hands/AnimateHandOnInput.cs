using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace com.LazyGames.DZ
{
    public class AnimateHandOnInput : MonoBehaviour
    {
        [SerializeField] private InputActionProperty _pinchAnimAction;
        [SerializeField] private InputActionProperty _gripAnimAction;
        [SerializeField] private Animator _handAnim;

        void Update()
        {
            float triggerValue = _pinchAnimAction.action.ReadValue<float>();
            _handAnim.SetFloat("Trigger", triggerValue);

            float gripValue = _gripAnimAction.action.ReadValue<float>();
            _handAnim.SetFloat("Grip", gripValue);
        }
    }
}