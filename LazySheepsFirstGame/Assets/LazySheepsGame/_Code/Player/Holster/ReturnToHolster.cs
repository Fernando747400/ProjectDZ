using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ReturnToHolster : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private BoxCollider _colliderOne;
    [SerializeField] private BoxCollider _colliderTwo;
    [SerializeField] private float _secondsToReturn = 10f;

    private Tween returnTween;
    private Rigidbody _rb;
    private bool _return = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_rb.isKinematic)
        {
            _colliderOne.enabled = false;
            _colliderTwo.enabled = false;
            _return = false;
            if(returnTween != null) returnTween.Kill();
            StopAllCoroutines();
        }
        if (!_rb.isKinematic) 
        {
            _colliderOne.enabled = true;
            _colliderTwo.enabled = true;
            _return = true;
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
            _colliderOne.enabled = true;
            _colliderTwo.enabled = true;
            if (returnTween != null) returnTween.Kill();
            yield return null;
        }
        if (_return)
        {
            _colliderOne.enabled = false;
            _colliderTwo.enabled = false;
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
            if (this.transform.position != _targetPosition.position) ReturnToTargetWithTween();
            return;
        });
    }
}
