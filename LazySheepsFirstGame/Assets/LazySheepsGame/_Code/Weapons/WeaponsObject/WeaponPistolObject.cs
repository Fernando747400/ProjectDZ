using System;
using com.LazyGames.DZ;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponPistolObject : WeaponObject
{

    #region Serialized Fields
    [Header("Pistol Object")]
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

    public override void Reload()
    {
        base.Reload();
        PlayAnimsWeapon(animaReloadName);
        // Debug.Log("Reload Pistol");
    }

    public override void Shoot()
    {
        base.Shoot();
        // Debug.Log("Shoot Pistol");
    }
    
    #endregion


    #region private methods
    
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
