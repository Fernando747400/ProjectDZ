using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerReferenceInit : MonoBehaviour
{
    [Header("Dependencies")]

    [Required]
    [SerializeField] private ReferencePool _playerReferences;
    [Required]
    [SerializeField] private XRInteractionManager _interactionManager;
    [Required]
    [SerializeField] private Component _componentToSearch;

    private List<GameObject> _references;

    private void OnEnable()
    {
        GetReferences();
        LinkReferences();
    }

    private void GetReferences()
    {
        _references = _playerReferences.SearchAllReferencesByComponent(_componentToSearch.GetType());
    }

    private void LinkReferences()
    {
        foreach (GameObject reference in _references) 
        { 
            reference.GetComponent<XRGrabInteractable>().interactionManager = _interactionManager;
        }
    }
}
