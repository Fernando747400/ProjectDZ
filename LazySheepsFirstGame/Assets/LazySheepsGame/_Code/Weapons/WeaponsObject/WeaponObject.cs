using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
using com.LazyGames.Dio;
using com.LazyGames.DZ;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace com.LazyGames
{
    public class WeaponObject : MonoBehaviour, IGeneralAggressor
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
        private RaycastHit _simulatedHit;
        

        private void OnEnable()
        {
            PrepareAgressor();
        }

        private void OnDisable()
        {
            InputShootActionRight.IntEvent -= (value) =>
            {
                HandleShootEvent(value);
            };
            InputShootActionLeft.IntEvent -= (value) =>
            {
                HandleShootEvent(value);
            };
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("HandLeft"))
            {
                currentHandHolding = HandShoot.Left;
                //Debug.Log("Hand Holder Enter".SetColor("#F1BE50"));
            }
            if (other.CompareTag("HandRight"))
            {
                currentHandHolding = HandShoot.Right;
                //Debug.Log("Hand Holder Enter".SetColor("#F1BE50"));
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) {
                Shoot();
            }
        }

        #region public methods

        public void OnSelectWeapon(SelectEnterEventArgs args)
        {
            //Debug.Log("OnSelectWeapon".SetColor("#F1BE50"));
            _isHoldingWeapon = true;
        }
        public void OnSelectExitWeapon(SelectExitEventArgs args)
        {
            //Debug.Log("OnSelectExitWeapon".SetColor("#50F155"));
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
            Debug.Log("Is Holding Weapon = " + currentHandHolding);
            Debug.Log("Shoot = " + value);

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
            _savedFirePosition = shootPoint.transform.position;
            RaycastHit hit;
            
            if (!Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, weaponData.MaxDistance ,Physics.DefaultRaycastLayers))
            {
                Debug.Log("No Hit".SetColor("#F95342"));
                Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward * weaponData.MaxDistance, Color.red, 1f);
                return;
            }
            //Collision Raycast
            _hitPosition = hit.point;
            BulletTravel();
        }
        #endregion
        
        
        
        protected virtual void BulletTravel()
        {
            // Debug.Log("BulletTravel".SetColor("#DB7AFF"));
            //StopAllCoroutines();
            Vector3 simulatedHitDir = _hitPosition - _savedFirePosition;
            Physics.Raycast(_savedFirePosition, simulatedHitDir.normalized,out _simulatedHit, weaponData.MaxDistance, weaponData.LayerMasks);
            Debug.DrawRay(_savedFirePosition, simulatedHitDir.normalized * weaponData.MaxDistance, Color.green, 1f);
        
            if (!TryGetGeneralTarget()) return;
             Debug.Log("Receive Damage ".SetColor("#4DF942"));
            _simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>().ReceiveAggression(Vector3.zero, 0, weaponData.Damage);
            //_simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>().ReceiveAggression(
            // (_simulatedHit.point - _savedFirePosition).normalized ,120,weaponData.Damage);
            SendAggression(true);
        }

        public bool TryGetGeneralTarget()
        {
            return _simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>() != null;
        }

        public void SendAggression(bool isTarget)
        {
            if(!TryGetGeneralTarget()) return;
            _simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>().ReceiveAggression(Vector3.zero, 0, weaponData.Damage);
        }
    }
    
    
    
}
