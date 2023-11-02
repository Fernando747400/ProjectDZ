using System.Collections;
using System.Collections.Generic;
using com.LazyGames.DZ;
using UnityEngine;

public class WeaponShotGunObject : WeaponObject
{
   [Header("ShotGun Object")]
   [SerializeField] private int positionShoots = 3;

   public override void Shoot()
   {
      base.Shoot();
      Debug.Log("Shoot ShotGun");
   }
}
