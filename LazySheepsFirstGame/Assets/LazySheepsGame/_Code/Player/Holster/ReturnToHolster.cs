using System;
using Autohand;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ReturnToHolster : MonoBehaviour
{
    [SerializeField] private Grabbable autoHandGrabbable;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _secondsToReturn = 2f;
    [SerializeField] private bool _isGordy = false;

    private Tween returnTween;
    private Rigidbody _rb;
    private bool _return = false;
    private bool _isInHand = false;

    private void OnEnable()
    {
        autoHandGrabbable.OnGrabEvent += IsGrabbed;
        autoHandGrabbable.OnReleaseEvent += IsDroped;
    }
    
    private void OnDisable()
    {
        autoHandGrabbable.OnGrabEvent -= IsGrabbed;
        autoHandGrabbable.OnReleaseEvent -= IsDroped;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isGordy)
        {
            if (_rb.isKinematic || _isInHand)
            {
                _return = false;
                if(returnTween != null) returnTween.Kill();
                StopAllCoroutines();
            }
            if (!_rb.isKinematic || !_isInHand) 
            {
                _return = true;
            }
        }
        else if(!_isGordy)
        {
            try
            {
                _rb = GetComponent<Rigidbody>();
            }
            catch (Exception e)
            {
                
            }
            
            if (_rb == null || _isInHand)
            {
                _return = false;
                if(returnTween != null) returnTween.Kill();
                StopAllCoroutines();
            }
            else if(_rb != null || !_isInHand)
            {
                _return = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ReturnToTargetAfterDelay());
    }

    IEnumerator ReturnToTargetAfterDelay()
    {
        if (!_return)
        {
            if (returnTween != null) returnTween.Kill();
            yield return null;
        }
        if (_return)
        {
            yield return new WaitForSeconds(_secondsToReturn);
            ReturnToTargetWithTween();
        }  
    }

    void ReturnToTargetWithTween()
    {
        returnTween = transform.DOMove(_targetPosition.position, 1f).OnComplete(() =>
        {
            _return = false;
            returnTween.Kill();
            StopAllCoroutines();
            if (!_rb.isKinematic)
            {
                ReturnToTargetWithTween();
            }
            return;
        });
    }
    
    private void IsGrabbed(Hand hand, Grabbable grabbable)
    {
        _isInHand = true;
    }
    
    private void IsDroped(Hand hand, Grabbable grabbable)
    {
        _isInHand = false;
    }
}
