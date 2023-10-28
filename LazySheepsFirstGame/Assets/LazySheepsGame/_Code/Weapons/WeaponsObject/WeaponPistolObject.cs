using com.LazyGames.DZ;
using UnityEngine;

public class WeaponPistolObject : WeaponObject
{
    [Header("Pistol Object")] [SerializeField]
    private InteractorReloadWeapon interactorReloadWeapon;


    private bool _isReloading = false;
    private Vector3 _initialHandReloadPosition;
    private Vector3 _currentHandReloadPosition;
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


    private void OnHandEnterCollider(Vector3 handPosition)
    {
        _initialHandReloadPosition = handPosition;
    }
    
    private void OnHandOutCollider(Vector3 handPosition)
    {
       _lastHandReloadPosition = handPosition;
    }
    
    private void OnHandStayCollider(Vector3 handPosition)
    {
        _currentHandReloadPosition = handPosition;
        if (_isReloading)
        {
            if (Vector3.Distance(_initialHandReloadPosition, _currentHandReloadPosition) >
                Vector3.Distance(_initialHandReloadPosition, _lastHandReloadPosition))
            {
                Debug.Log("Reload");
            }
        }
    }
    
    

}
