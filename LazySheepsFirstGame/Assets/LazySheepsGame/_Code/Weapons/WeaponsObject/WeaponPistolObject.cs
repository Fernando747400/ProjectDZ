using System;
using com.LazyGames.DZ;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponPistolObject : WeaponObject
{
    [Header("Pistol Object")]
    [SerializeField] private InteractorVectorReloadWeapon interactorVectorReloadWeapon;
    [SerializeField] private string animaReloadName = "Reload";
    [SerializeField] private float velocityTarget = 0.5f;


    private bool _isReloading = false;
    private Vector3 _initialHandReloadPosition;
    private Vector3 _lastHandReloadPosition;
    private float _elapsedTime;
    
    
    public override void InitializeWeapon()
    {
        base.InitializeWeapon();
        interactorVectorReloadWeapon.OnEnter += OnHoveredEnter;
        interactorVectorReloadWeapon.OnExit += OnHoveredExit;
    }

    public override void Reload()
    {
        base.Reload();
        PlayAnimsWeapon(animaReloadName);
        Debug.Log("Reload Pistol");
    }

    public override void Shoot()
    {
        base.Shoot();
        // Debug.Log("Shoot Pistol");
    }

    private void OnHoveredEnter(Vector3 position)
    {
        if(!IsHoldingWeapon) return;
        
        _initialHandReloadPosition = position;
        _isReloading = true;
    }
    
    private void OnHoveredExit(Vector3 position)
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
        Debug.Log($"Velocity: {velocity}");

        if (velocity > velocityTarget)
        {
            Reload();
        }
        
        _elapsedTime = 0;
        _initialHandReloadPosition = Vector3.zero;
        _lastHandReloadPosition = Vector3.zero;
    }

    private void FixedUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimsWeapon(animaReloadName);
        }
        if(!_isReloading) return;
        _elapsedTime += Time.deltaTime;
    }
}
