using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
using com.LazyGames.Dio;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace com.LazyGames
{
    public class WeaponObject : AgressorBase
    {
        #region SerializedFields
        [Header("Weapon Object")]
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private IntEventChannelSO InputShootActionRight;
        [SerializeField] private IntEventChannelSO InputShootActionLeft;
        [SerializeField] private ParticleSystem shootParticle;
        
        
        [Header("Hand Holder")]
        [SerializeField] private HandShoot currentHandHolding;

        #endregion
        
        private float _travelTime = 0.3f;
        private Vector3 _hitPosition;
        private Vector3 _savedFirePosition;
        private bool _isHoldingWeapon = false;
        

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
            InputShootActionRight.IntEvent -= (value) =>
            {
                HandleShootEvent(value);
                Debug.Log("Right desuscript");
            };
            InputShootActionLeft.IntEvent -= (value) =>
            {
                HandleShootEvent(value);
                Debug.Log("Left desuscript");
            };


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("HandLeft"))
            {
                currentHandHolding = HandShoot.Left;
                Debug.Log("Hand Holder Enter".SetColor("#F1BE50"));
            }
            if (other.CompareTag("HandRight"))
            {
                currentHandHolding = HandShoot.Right;
                Debug.Log("Hand Holder Enter".SetColor("#F1BE50"));
            }
        }

        #endregion

        #region public methods

        public void OnSelectWeapon(SelectEnterEventArgs args)
        {
            Debug.Log("OnSelectWeapon".SetColor("#F1BE50"));
            _isHoldingWeapon = true;
        }
        public void OnSelectExitWeapon(SelectExitEventArgs args)
        {
            Debug.Log("OnSelectExitWeapon".SetColor("#50F155"));
            _isHoldingWeapon = false;
            currentHandHolding = HandShoot.None;
        }

        #endregion
        
        
        #region private methods

        private void PrepareAgressor()
        {
            InputShootActionRight.IntEvent += HandleShootEvent;
            InputShootActionLeft.IntEvent += HandleShootEvent;
        }

        private void HandleShootEvent(int value)
        {
            Debug.Log("Is Holding Weapon = " + _isHoldingWeapon.ToString().SetColor("#F1BE50"));

            if(currentHandHolding == HandShoot.None) return;
            if (value != (int)currentHandHolding) return;
            if (!_isHoldingWeapon) return;
            
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

        private void StartConstantShoot()
        {
            
        }
        private void Shoot()
        {
            shootParticle.Play();
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
