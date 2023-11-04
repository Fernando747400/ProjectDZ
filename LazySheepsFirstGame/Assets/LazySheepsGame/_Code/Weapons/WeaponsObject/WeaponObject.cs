using System.Collections;
using UnityEngine;
using com.LazyGames.Dio;
using Lean.Pool;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace com.LazyGames.DZ
{
    public class WeaponObject : WeaponBase, IGeneralAggressor
    {
        #region SerializedFields

        [Header("Weapon Object")] [SerializeField]
        private WeaponData weaponData;

        [SerializeField] private Transform shootPoint;

        [Header("Input Actions")] [SerializeField]
        private IntEventChannelSO InputShootActionRight;

        [SerializeField] private IntEventChannelSO InputShootActionLeft;

        [Header("Hand Object")]
        [SerializeField] private BoolEventChannelSO isInHandChannel;
        [SerializeField] private HandEventChannelSO handHolderEventSO;
        [SerializeField] private HandHolder currentHandHolding;

        [Header("UI")] [SerializeField] private GameObject weaponUIGO;

        [Header("Particles")] [SerializeField] private float timeToDespawnPart = 1f;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ParticleSystem hitLaserParticle;

        [Header("Reload")]
        [SerializeField] private Animator reloadAnimator;
        [SerializeField] private string animaNeedReloadName = "NeedReload";
        
        [Header("XRGrabInteractable")]
        [SerializeField] private XRGrabInteractable _grabInteractable;
        #endregion

        #region public variables
        
        public int CurrentAmmo
        {
            get => _currentAmmo;
            protected set => _currentAmmo = value;
        }
        public WeaponData WeaponData => weaponData;
        
        
        public bool IsHoldingWeapon => _isHoldingWeapon;

        #endregion
        #region private variables
        
        private int _currentAmmo;
        private float _travelTime = 0.3f;
        private Vector3 _hitPosition;
        private Vector3 _savedFirePosition;
        private bool _isHoldingWeapon = false;
        private RaycastHit _simulatedHit;
        private WeaponUI _weaponUI;
        private float _lineRendererMaxDistance = 10f;
        private Rigidbody _rigidbody;

        #endregion


        #region Unity Methods
        
        private void OnDestroy()
        {
            // Debug.Log("OnDestroy = ".SetColor("#F95342") + weaponData.ID);
            
            InputShootActionRight.IntEvent -= HandleShootEvent;
            InputShootActionLeft.IntEvent -= HandleShootEvent;
            isInHandChannel.BoolEvent -= CheckIsInHand;
            handHolderEventSO.HandHolderEvent -= CheckCurrentHandHolder;
        }

        private void Start()
        {
            PrepareAgressor();
            InitializeWeapon();
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     PlayAnimsWeapon(animaNeedReloadName);
            // }

            if (lineRenderer.enabled)
            {
                Ray ray = new Ray(shootPoint.transform.position, shootPoint.transform.forward);
                bool cast = Physics.Raycast(ray, out RaycastHit hit, _lineRendererMaxDistance);
                Vector3 endPosition = cast ? hit.point : ray.GetPoint(_lineRendererMaxDistance);
                lineRenderer.SetPosition(0, shootPoint.transform.position);
                lineRenderer.SetPosition(1, endPosition);
                hitLaserParticle.transform.position = endPosition;
            }
        }

        #endregion

        #region public methods

        public void EnableGrabInteractable(bool value)
        {
            _grabInteractable.enabled = value;
        }
        public override void Reload()
        {
            DoReload();
        }
        public override void Shoot()
        {
            // Debug.Log("Shoot = ".SetColor("#16CCF5") + weaponData.ID);
            _savedFirePosition = shootPoint.transform.position;
            _currentAmmo--;
            _weaponUI.UpdateTextMMO(CurrentAmmo);
            PlayParticleShoot();
           
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
        }

        public void PlayAnimsWeapon(string nameAnim)
        {
            reloadAnimator.Play(nameAnim);
        }
        
        public override void InitializeWeapon()
        {
            EnableBeamLaser(false);
            CurrentAmmo = weaponData.MaxAmmo;

            if(_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            if(_grabInteractable ==  null) _grabInteractable = GetComponent<XRGrabInteractable>();
            
            if(reloadAnimator == null) reloadAnimator = GetComponent<Animator>();
            reloadAnimator.runtimeAnimatorController = weaponData.ReloadAnimator;
            
            if(_weaponUI == null) _weaponUI = transform.GetComponent<WeaponUI>();
            
            _weaponUI.UpdateTextMMO(CurrentAmmo);
            _lineRendererMaxDistance = weaponData.MaxDistance;
        }
        
        #endregion

        
        #region private methods
        private void PrepareAgressor()
        {
            // Debug.Log("PrepareAgressor = ".SetColor("#F1BE50") +  weaponData.ID);
            InputShootActionRight.IntEvent += HandleShootEvent;
            InputShootActionLeft.IntEvent += HandleShootEvent;
            isInHandChannel.BoolEvent += CheckIsInHand;
            handHolderEventSO.HandHolderEvent += CheckCurrentHandHolder;
        }

        private void CheckIsInHand(bool isInHand)
        {
           _isHoldingWeapon = isInHand;
           
           if (_isHoldingWeapon)
           {
               weaponUIGO.SetActive(true);
               EnableBeamLaser(true);

           }
           else
           {
               currentHandHolding = HandHolder.None;
               // transform.parent = null;
               weaponUIGO.SetActive(false);
               EnableBeamLaser(false);
           }
        }
        
        private void CheckCurrentHandHolder(HandHolder handHolder)
        {
            currentHandHolding = handHolder;

            // if (currentHandHolding == HandHolder.HandLeft)
            // {
            //     // transform.SetParent(PlayerManager.Instance.LeftHandAttachPoint);
            // }else
            // if (currentHandHolding == HandHolder.HandRight)
            // {
            //     // transform.SetParent(PlayerManager.Instance.RightHandAttachPoint);
            // }
            
        }
        private void HandleShootEvent(int value)
        {
            // Debug.Log("HandleShootEvent".SetColor("#F1BE50"));
            if(currentHandHolding == HandHolder.None) return;
            if (value != (int)currentHandHolding) return;
            if (!_isHoldingWeapon) return;

            if (CurrentAmmo <= 0)
            {
                CallNeedReload();
                return;
            }
            
            _weaponUI.NeedReload(false);

            switch (weaponData.WeaponType) 
            { 
                case WeaponType.Pistol: 
                    Shoot();
                    break;
                case WeaponType.AutomaticRifle: 
                    StartConstantShoot(); 
                    break;
                case WeaponType.Shotgun:
                    Shoot();
                    break;
            }
            

        }
        private void StartConstantShoot()
        {
            
        }
        
        protected virtual void BulletTravel()
        {
            Vector3 simulatedHitDir = _hitPosition - _savedFirePosition;
            Physics.Raycast(_savedFirePosition, simulatedHitDir.normalized,out _simulatedHit, weaponData.MaxDistance, weaponData.LayerMasks);
            Debug.DrawRay(_savedFirePosition, simulatedHitDir.normalized * weaponData.MaxDistance, Color.green, 1f);
            if(_simulatedHit.distance > weaponData.MaxDistance) return;
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
        private void CallNeedReload()
        {
            _weaponUI.NeedReload(true);
            _weaponUI.UpdateTextMMO(CurrentAmmo);
          
            // PlayAnimsWeapon(weaponData.AnimationsReloads.Find(x => x.nameAnimation == animaNeedReloadName).nameAnimation);
            PlayAnimsWeapon(animaNeedReloadName);
            
            // Debug.Log("Need Reload".SetColor("#F95342"));
        }
        private void DoReload()
        {
            CurrentAmmo = weaponData.MaxAmmo;
            _weaponUI.NeedReload(false);
            _weaponUI.UpdateTextMMO(CurrentAmmo);
        }
        private void EnableBeamLaser(bool enable)
        {
            lineRenderer.gameObject.SetActive(enable);
            lineRenderer.enabled = enable;
            
            if(enable) hitLaserParticle.Play();
            else hitLaserParticle.Stop();
        }

        private void DoRecoilWeapon()
        {
            
        }
        #endregion


        #region IGeneralAggressor

        public void SendAggression()
        {
            if (_simulatedHit.collider == null)
            {
                Debug.Log("No Hit".SetColor("#F95342"));
                return;
            }
            if (!_simulatedHit.collider.gameObject.TryGetComponent<IGeneralTarget>(out var generalTarget)) return;
            generalTarget.ReceiveAggression(_simulatedHit.point, 23,weaponData.Damage);
            Debug.Log("Send Aggression to  =   ".SetColor("#F1BE50") + _simulatedHit.collider.gameObject.name);
        }
        #endregion

        
    }
    
    
    
}
