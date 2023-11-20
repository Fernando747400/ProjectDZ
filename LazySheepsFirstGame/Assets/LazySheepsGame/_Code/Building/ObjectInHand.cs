using Autohand;
using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using com.LazyGames.DZ;
using NaughtyAttributes;

public class ObjectInHand : MonoBehaviour
{
    [Header("Dependencies")]
    // [SerializeField] private XRGrabInteractable xrGrabInteractable;
    [SerializeField] private Grabbable autoHandGrabbable;
    [SerializeField] private BoolEventChannelSO isInHandChannel;
    [SerializeField] private bool _holdEventActive;
    [ShowIf("_holdEventActive")]
    [SerializeField] private HandEventChannelSO handHolderEventSO;

    private void OnEnable()
    {
        autoHandGrabbable.OnGrabEvent += GrabbedObject;
        autoHandGrabbable.OnReleaseEvent += DroppedObject;
    }

    private void OnDisable()
    {
        autoHandGrabbable.OnGrabEvent -= GrabbedObject;
        autoHandGrabbable.OnReleaseEvent -= DroppedObject;
    }

    private void GrabbedObject(Hand hand, Grabbable grabbable)
    {
        
        if(hand.left || !hand.left)
        {
            isInHandChannel.RaiseEvent(true);
            
            if (handHolderEventSO != null) handHolderEventSO.RaiseEvent(GetHandHolder(hand));
            
            Debug.Log("Object ".SetColor("#F5DD16")+ transform.name +  " Grabbed by =".SetColor("#F5DD16") + GetHandHolder(hand));
        }
        
        
        
    }

    private void DroppedObject(Hand hand, Grabbable grabbable)
    {
        if(hand.left || !hand.left)
        {
            isInHandChannel.RaiseEvent(false);
            if (handHolderEventSO != null) handHolderEventSO.RaiseEvent(GetHandHolder(hand));
            Debug.Log("Object ".SetColor("#F5DD16") + transform.name + " Dropped by =".SetColor("#F5DD16") + GetHandHolder(hand));

        }
    }
    
    private HandHolder GetHandHolder(Hand hand)
    {
        HandHolder handHolder = HandHolder.None;
        if (hand.left)
        {
            handHolder = HandHolder.HandLeft;
        }
        else 
        {
            handHolder = HandHolder.HandRight;
        }
       
        
        return handHolder;
    }
}
