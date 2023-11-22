using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.DZ;
using UnityEngine;

public class WeaponShotGunObject : WeaponObject
{
   #region Serialized Fields

   [Header("ShotGun Object")]
   [SerializeField] private List<Transform> positionShoots = new List<Transform>();
   [SerializeField] private InteractorVectorReloadWeapon interactorVectorReloadWeapon;
   [SerializeField] private string animaReloadName = "Reload";
   [SerializeField] private float velocityTarget = 0.5f;

   #endregion
   
   
   #region private variables
   private bool _isReloading = false;
   private Vector3 _initialHandReloadPosition;
   private Vector3 _lastHandReloadPosition;
   private float _elapsedTime;
   #endregion
   
   #region Unity methods
    
   private void FixedUpdate()
   {
      if(!_isReloading) return;
      _elapsedTime += Time.deltaTime;
   }

   private void OnDestroy()
   {
      interactorVectorReloadWeapon.OnEnter -= OnHoveredWeaponEnter;
      interactorVectorReloadWeapon.OnExit -= OnHoveredWeaponExit;
   }

   #endregion

   
   #region public methods
   public override void InitializeWeapon()
   {
      base.InitializeWeapon();
      interactorVectorReloadWeapon.OnEnter += OnHoveredWeaponEnter;
      interactorVectorReloadWeapon.OnExit += OnHoveredWeaponExit;
   }

   public override void Shoot()
   {
      base.Shoot();
      // Debug.Log("Shoot ShotGun".SetColor("#6BE720"));
      ShotGunShoot(positionShoots);

   }

   public override void PhysicShoot()
   {
      base.PhysicShoot();
      // Debug.Log("Shoot ShotGun".SetColor(""));

   }

   public override void Reload()
   {
      base.Reload();
      PlayAnimsWeapon(animaReloadName);
      // Debug.Log("Reload ShotGun");
   }
   #endregion
   
   #region private methods

   private void ShotGunShoot(List<Transform> transforms)
   {
      foreach (var pos in transforms)
      {
         _savedFirePosition = pos.position;
         PhysicShoot();
      }
   }
   
   private void OnHoveredWeaponEnter(Vector3 position)
   {
      if(!IsHoldingWeapon) return;
        
      _initialHandReloadPosition = position;
      _isReloading = true;
   }
    
   private void OnHoveredWeaponExit(Vector3 position)
   {
      if(!IsHoldingWeapon) return;
        
      _lastHandReloadPosition = position;
      _isReloading = false;
      CalculateVelocity();
   }

   private void CalculateVelocity()
   {
      float distance = Vector3.Distance(_initialHandReloadPosition, _lastHandReloadPosition);
      float velocity = distance / _elapsedTime;
      // Debug.Log($"Velocity: {velocity}");

      if (velocity > velocityTarget)
      {
         Reload();
      }
        
      _elapsedTime = 0;
      _initialHandReloadPosition = Vector3.zero;
      _lastHandReloadPosition = Vector3.zero;
   }
    
    

   #endregion
}
