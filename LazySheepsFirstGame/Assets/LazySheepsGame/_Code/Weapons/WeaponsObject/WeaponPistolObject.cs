using com.LazyGames.DZ;
using UnityEngine;

public class WeaponPistolObject : WeaponObject
{
    [Header("Pistol Object")]
    [SerializeField] private InteractorReloadWeapon interactorReloadWeapon;
    [SerializeField] private GameObject reloadTarget;


    private bool _isReloading = false;
    private Vector3 _initialHandReloadPosition;
    private Vector3 _lastHandReloadPosition;
    
    public override void InitializeWeapon()
    {
        base.InitializeWeapon();
        // interactorReloadWeapon.OnHandEnter += OnHandEnterCollider;
    }

    public override void Reload()
    {
        base.Reload();
        Debug.Log("Reload Pistol");
    }

    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Shoot Pistol");
    }
    

}
