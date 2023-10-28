using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public abstract void InitializeWeapon();
    public abstract void Reload();
    public abstract void Shoot();
    
}
