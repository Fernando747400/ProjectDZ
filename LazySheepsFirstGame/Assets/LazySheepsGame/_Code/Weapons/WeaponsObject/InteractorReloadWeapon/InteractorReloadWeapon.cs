using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorReloadWeapon : MonoBehaviour
{
    [SerializeField] private Collider collider;

    public event Action<Vector3> OnHandEnter;
    public event  Action<Vector3> OnHandOut;
    public event Action<Vector3> OnHandStay; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            OnHandEnter?.Invoke(other.transform.position);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            OnHandOut?.Invoke(other.transform.position);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            OnHandStay?.Invoke(other.transform.position);
        }
    }
    
}
