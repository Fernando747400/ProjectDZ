using System.Collections.Generic;
using com.LazyGames.DZ;
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
    
    private List<GameObject> _references;

    private void OnEnable()
    {
        GetReferences();
        LinkReferences();
        _playerReferences.ClearAllReferences();
    }

    private void GetReferences()
    {
        _references = _playerReferences.SearchAllReferencesByComponent(typeof(XRBaseInteractable));
    }

    private void LinkReferences()
    {
        foreach (GameObject reference in _references) 
        { 
            reference.GetComponent<XRBaseInteractable>().interactionManager = _interactionManager;          
        }
    }
}
