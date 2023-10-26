using com.LazyGames.DZ;
using UnityEngine;

public class WeaponPistolObject : WeaponObject
{
    
    // [SerializeField] private string animaReloadName;
    
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
