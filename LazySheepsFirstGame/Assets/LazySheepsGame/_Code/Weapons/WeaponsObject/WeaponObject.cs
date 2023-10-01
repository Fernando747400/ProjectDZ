using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;

namespace com.LazyGames
{
    public class WeaponObject : AgressorBase
    {
        #region SerializedFields
        [Header("Weapon Object")]
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform shootPoint;

        #endregion
        
        private float _travelTime = 0.3f;
        private Vector3 _hitPosition;
        private Vector3 _savedFirePosition;

        #region unity methods

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        #endregion
        
        #region private methods
        
        private void Shoot()
        {
            Debug.Log("Shoot".SetColor("#DB7AFF"));
            StopAllCoroutines();
            _savedFirePosition = transform.position;
            if (!Physics.Raycast( transform.position, transform.forward, out RaycastHit hit, weaponData.MaxDistance,
                    Physics.DefaultRaycastLayers)) return;
            
            _hitPosition = hit.point;
            _travelTime = hit.distance / (weaponData.BulletSpeed) * Time.fixedDeltaTime;
            StartCoroutine(DelayBulletTravel());
        }
        #endregion
        
        private IEnumerator DelayBulletTravel()
        {
            yield return new WaitForSeconds(_travelTime);
            BulletTravel();
        }
        
        protected virtual void BulletTravel()
        {
            StopAllCoroutines();
            Vector3 simulatedHitDir = _hitPosition - _savedFirePosition;
            Physics.Raycast(_savedFirePosition, simulatedHitDir.normalized,out RaycastHit simulatedHit, weaponData.MaxDistance, weaponData.LayerMask);
        
            if (!TryGetGeneralTarget(simulatedHit.collider.gameObject)) return;
            simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>().ReceiveRaycast(weaponData.Damage);

        }
        
    }
}
