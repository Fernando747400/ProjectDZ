using System;
using Obvious.Soap;
using UnityEngine;
using Autohand;

public class GordyInteract : MonoBehaviour
{
    [SerializeField] private FloatVariable _mana;
    [SerializeField] private Grabbable autoHandGrabbable;
    private GameObject _manaPortal;
    private bool _isGrabbed = false;
    private int _currentCollition = 0;

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

    private void AddToMana()
    {
        _mana.Value += 1;
    }
    
    private void GrabbedObject(Hand hand, Grabbable grabbable)
    {
        _isGrabbed = true;
    }
    
    private void DroppedObject(Hand hand, Grabbable grabbable)
    {
        _isGrabbed = false;
        _mana.Value = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ManaPortal" && _isGrabbed)
        {
            CountCollition();
            if(_manaPortal != null) _manaPortal.SetActive(true);
            _manaPortal = other.gameObject;
            _manaPortal.SetActive(false);
        }
    }

    private void CountCollition()
    {
        _currentCollition++;
        if (_currentCollition >= 4)
        {
            AddToMana();
            _currentCollition = 0;
        }
    }
}
