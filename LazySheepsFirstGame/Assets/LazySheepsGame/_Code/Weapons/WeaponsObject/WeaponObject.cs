using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.LazyGames;
using com.LazyGames.Dio;
using com.LazyGames.DZ;
using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace com.LazyGames.DZ
{
    public class WeaponObject : MonoBehaviour, IGeneralAggressor
    {
        #region SerializedFields
        [Header("Weapon Object")]
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private IntEventChannelSO InputShootActionRight;
        [SerializeField] private IntEventChannelSO InputShootActionLeft;
        
        [SerializeField] private BoolEventChannelSO isInHandChannel;
        [SerializeField] private HandEventChannelSO handHolderEventSO;
        
        [Header("UI")]
        [SerializeField] private GameObject weaponUIGO;
        
        [Header("Hand Holder")]
        [SerializeField] private HandHolder currentHandHolding;
        
        [Header("Particles")]
        [SerializeField] private float timeToDespawnPart = 1f;


        [Header("Test")] 
        [SerializeField] private Transform sphereTarget;

        #endregion

        #region public variables
        
        public int CurrentAmmo
        {
            get => _currentAmmo;
            protected set => _currentAmmo = value;
        }

        #endregion
        #region private variables
        
        private int _currentAmmo;
        private float _travelTime = 0.3f;
        private Vector3 _hitPosition;
        private Vector3 _savedFirePosition;
        private bool _isHoldingWeapon = false;
        private RaycastHit _simulatedHit;
        private WeaponUI _weaponUI;

        #endregion


        #region Unity Methods
        
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
            
            isInHandChannel.BoolEvent -= CheckIsInHand;
            handHolderEventSO.HandHolderEvent -= CheckCurrentHandHolder;
        }

        private void Start()
        {
            InitializeWeapon();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) {
                Shoot();
            }
        }

        #endregion

        #region public methods
        
        public void OnSelectWeapon(SelectEnterEventArgs args)
        {
            if (_isHoldingWeapon)
            {
                weaponUIGO.SetActive(true);
            }
        }
        
        public void OnSelectExitWeapon(SelectExitEventArgs args)
        {
            _isHoldingWeapon = false;
            currentHandHolding = HandHolder.None;
            weaponUIGO.SetActive(false);
        }

        #endregion
        
        
        #region private methods

        private void InitializeWeapon()
        {
            CurrentAmmo = weaponData.MaxAmmo;
            _weaponUI = transform.GetComponent<WeaponUI>();
            _weaponUI.UpdateTextMMO(CurrentAmmo);
            
        }
        private void PrepareAgressor()
        {
            InputShootActionRight.IntEvent += HandleShootEvent;
            InputShootActionLeft.IntEvent += HandleShootEvent;
            isInHandChannel.BoolEvent += CheckIsInHand;
            handHolderEventSO.HandHolderEvent += CheckCurrentHandHolder;
        }

        private void CheckIsInHand(bool isInHand)
        {
           _isHoldingWeapon = isInHand;
        }
        
        private void CheckCurrentHandHolder(HandHolder handHolder)
        {
            currentHandHolding = handHolder;
        }
        private void HandleShootEvent(int value)
        {
            if(currentHandHolding == HandHolder.None) return;
            if (value != (int)currentHandHolding) return;
            if (!_isHoldingWeapon) return;

            if (CurrentAmmo <= 0)
            {
                CallReload();
                return;
            }
            
            switch (weaponData.WeaponType) 
            { 
                case WeaponType.Pistol: 
                    _weaponUI.NeedReload(false);
                    Shoot();
                    break;
                case WeaponType.AutomaticRifle: 
                    StartConstantShoot(); 
                    break;
            }
            
            Debug.Log("Shoot = ".SetColor("#16CCF5"));

        }

        private void StartConstantShoot()
        {
            
        }
        private void Shoot()
        {
            _savedFirePosition = shootPoint.transform.position;
            _currentAmmo--;
            RaycastHit hit;
            if (!Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, weaponData.MaxDistance ,Physics.DefaultRaycastLayers))
            {
                // Debug.Log("No Hit".SetColor("#F95342"));
                Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward * weaponData.MaxDistance, Color.red, 1f);
                return;
            }
            //Collision Raycast
            _hitPosition = hit.point;
            BulletTravel();
            
            _weaponUI.UpdateTextMMO(CurrentAmmo);
            PlayParticleShoot();

        }
        
        protected virtual void BulletTravel()
        {
            Vector3 simulatedHitDir = _hitPosition - _savedFirePosition;
            Physics.Raycast(_savedFirePosition, simulatedHitDir.normalized,out _simulatedHit, weaponData.MaxDistance, weaponData.LayerMasks);
            Debug.DrawRay(_savedFirePosition, simulatedHitDir.normalized * weaponData.MaxDistance, Color.green, 1f);
        
            if (!TryGetGeneralTarget()) return;
            SendAggression();
        }
        private void PlayParticleShoot()
        {
            GameObject shootParticleObject = LeanPool.Spawn(weaponData.ShootParticle);
            shootParticleObject.transform.position = shootPoint.transform.position;
            StartCoroutine(DespawnParticle(shootParticleObject));
        }
        
        private IEnumerator DespawnParticle(GameObject particle)
        {
            yield return new WaitForSeconds(timeToDespawnPart);
            LeanPool.Despawn(particle);
        }

        private void CallReload()
        {
            _weaponUI.NeedReload(true);
            Debug.Log("Reload".SetColor("#F95342"));
        }
        
        #endregion


        #region IGeneralAggressor
        public bool TryGetGeneralTarget()
        {
            if(_simulatedHit.collider != null)
            {
                if(sphereTarget != null) sphereTarget.position = _simulatedHit.point;
                return _simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>() != null;
            }

            return false;

        }

        public void SendAggression()
        {
            _simulatedHit.collider.gameObject.GetComponent<IGeneralTarget>().ReceiveAggression(_simulatedHit.point, 0,weaponData.Damage);
            // Debug.Log("Send Aggression to  =   ".SetColor("#F1BE50") + _simulatedHit.collider.gameObject.name);

        }
        #endregion

        
    }
    
    
    
}
