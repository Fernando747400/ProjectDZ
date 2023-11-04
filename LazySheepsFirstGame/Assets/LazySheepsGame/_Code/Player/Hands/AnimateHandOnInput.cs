using UnityEngine;
using UnityEngine.InputSystem;


namespace com.LazyGames.DZ
{
    public class AnimateHandOnInput : MonoBehaviour
    {
        [SerializeField] private InputActionProperty _gripAnimAction;
        [SerializeField] private InputActionProperty _triggerAnimAction;
        [SerializeField] private InputActionProperty _indexAnimAction;
        [SerializeField] private InputActionProperty _thumbAnimAction;
        [SerializeField] private Animator _handAnim;

        void Update()
        {
            float triggerValue = _gripAnimAction.action.ReadValue<float>();
            _handAnim.SetFloat("Trigger", triggerValue);

            float gripValue = _triggerAnimAction.action.ReadValue<float>();
            _handAnim.SetFloat("Grip", gripValue);

            float IndexValue = _indexAnimAction.action.ReadValue<float>();
            _handAnim.SetFloat("Index", IndexValue);

            float ThumbValue = _thumbAnimAction.action.ReadValue<float>();
            _handAnim.SetFloat("Thumb", ThumbValue);
        }
    }
}