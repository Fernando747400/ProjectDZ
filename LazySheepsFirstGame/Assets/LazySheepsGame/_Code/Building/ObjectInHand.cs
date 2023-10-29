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
    [SerializeField] private XRGrabInteractable xrGrabInteractable;
    [SerializeField] private BoolEventChannelSO isInHandChannel;
    [SerializeField] private bool _holdEventActive;
    [ShowIf("_holdEventActive")]
    [SerializeField] private HandEventChannelSO handHolderEventSO;

    private void OnEnable()
    {
        xrGrabInteractable.selectEntered.AddListener(GrabbedObject);
        xrGrabInteractable.selectExited.AddListener(DroppedObject);
    }

    private void OnDisable()
    {
        xrGrabInteractable.selectEntered?.RemoveListener(GrabbedObject);
        xrGrabInteractable.selectExited?.RemoveListener(DroppedObject);
    }

    private void GrabbedObject(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("HandLeft") || args.interactorObject.transform.CompareTag("HandRight"))
        {
            isInHandChannel.RaiseEvent(true);
            if (handHolderEventSO != null) handHolderEventSO.RaiseEvent(GetHandHolder(args.interactorObject));
            
            Debug.Log("Object ".SetColor("#F5DD16")+ transform.name +  " Grabbed by =".SetColor("#F5DD16") + GetHandHolder(args.interactorObject));
        }
    }

    private void DroppedObject(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("HandLeft") || args.interactorObject.transform.CompareTag("HandRight"))
        {
            isInHandChannel.RaiseEvent(false);
            if (handHolderEventSO != null) handHolderEventSO.RaiseEvent(GetHandHolder(args.interactorObject));
            Debug.Log("Object ".SetColor("#F5DD16") + transform.name + " Dropped by =".SetColor("#F5DD16") + GetHandHolder(args.interactorObject));

        }
    }
    
    private HandHolder GetHandHolder(IXRSelectInteractor interactor)
    {
        HandHolder handHolder = HandHolder.None;
        string interactorTag = interactor.transform.tag;
        switch (interactorTag)
        {
            case "HandLeft":
                handHolder = HandHolder.HandLeft;
                break;
            case "HandRight":
                handHolder = HandHolder.HandRight;
                break;
        }
        
        return handHolder;
    }
}
