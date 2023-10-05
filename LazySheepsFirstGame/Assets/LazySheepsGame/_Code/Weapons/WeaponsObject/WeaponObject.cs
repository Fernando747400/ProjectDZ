using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine.Serialization;

namespace com.LazyGames
{
    public class WeaponObject : AgressorBase
    {
        #region SerializedFields
        [Header("Weapon Object")]
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private FloatEventChannelSO InputShootActionRight;

        #endregion
        
        private float _travelTime = 0.3f;
        private Vector3 _hitPosition;
        private Vector3 _savedFirePosition;

        #region unity methods

        private void Start()
        {
            PrepareAgressor();
        }

        private void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     Shoot();
            // }
        }

        private void OnDisable()
        {
            InputShootActionRight.FloatEvent -= value =>
            {
                if(value >= 1)  Shoot();
            };
        }

        #endregion
        
        #region private methods

        private void PrepareAgressor()
        {
            InputShootActionRight.FloatEvent += value =>
            {
                if (value > 0)
                {
                    switch (weaponData.WeaponType)
                    {
                        case WeaponType.Pistol:
                            Shoot();
                            break;
                        
                        case WeaponType.AutomaticRifle:
                            StartConstantShoot();
                            break;
                    }
                }
            };
        }


        private void StartConstantShoot()
        {
            
        }
        private void Shoot()
        {
            StopAllCoroutines();
            _savedFirePosition = shootPoint.transform.position;
            RaycastHit hit;
            
            if (!Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, weaponData.MaxDistance ,Physics.DefaultRaycastLayers))
            {
                // Debug.Log("No Hit".SetColor("#F95342"));
                Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward * weaponData.MaxDistance, Color.red, 1f);
                return;
            }
            //Collision Raycast
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
            // Debug.Log("BulletTravel".SetColor("#DB7AFF"));
            StopAllCoroutines();
            Vector3 simulatedHitDir = _hitPosition - _savedFirePosition;
            Physics.Raycast(_savedFirePosition, simulatedHitDir.normalized,out RaycastHit simulatedHit, weaponData.MaxDistance, weaponData.LayerMasks);
            Debug.DrawRay(_savedFirePosition, simulatedHitDir.normalized * weaponData.MaxDistance, Color.green, 1f);
        
            if (!TryGetGeneralTarget(simulatedHit.collider.gameObject)) return;
            // Debug.Log("Receive Damage ".SetColor("#4DF942"));
            simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>().ReceiveRaycast(weaponData.Damage);

        }
        
    }
}
