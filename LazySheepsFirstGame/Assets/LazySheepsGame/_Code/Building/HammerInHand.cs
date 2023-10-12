using com.LazyGames.Dio;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HammerInHand : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private XRGrabInteractable _hammerInteractor;
    [SerializeField] private BoolEventChannelSO _hammerInHandChannel;

    private void OnEnable()
    {
        _hammerInteractor.selectEntered.AddListener(GrabbedHammer);
    }

    private void OnDisable()
    {
        _hammerInteractor.selectEntered.AddListener(DroppedHammer);
    }

    public void GrabbedHammer(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("HandLeft") || args.interactableObject.transform.CompareTag("HandRight"))
        {
            _hammerInHandChannel.RaiseEvent(true);
            Debug.Log("GrabbedHammer");
        }
    }

    private void DroppedHammer(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("HandLeft") || args.interactableObject.transform.CompareTag("HandRight"))
        {
            _hammerInHandChannel.RaiseEvent(false);
            Debug.Log("DroppedHammer");
        }
    }
}
