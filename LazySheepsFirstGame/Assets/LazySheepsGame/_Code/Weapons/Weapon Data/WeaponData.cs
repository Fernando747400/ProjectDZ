using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData", order = 0)]
    public class WeaponData : AgressorData
    {
        [Header("Weapon Data")] [SerializeField]
        private WeaponType weaponType;

        [SerializeField] private float bulletSpeed;
        [SerializeField] private float maxDistance;
        [SerializeField] private GameObject shootParticle;
        [SerializeField] private float cooldownPerShot;
        [SerializeField] private float delayReload;
        [SerializeField] private int maxAmmo;
        [SerializeField] private RuntimeAnimatorController reloadAnimator;
        [SerializeField] private List<AnimationsReloads> animationsReloads;
        
        [Header("Cost")]
        [SerializeField] private int cost;

        #region public variables

        public WeaponType WeaponType => weaponType;

        public float BulletSpeed
        {
            get => bulletSpeed;
        }

        public float MaxDistance
        {
            get => maxDistance;
        }
        public int Cost => cost;

        public GameObject ShootParticle => shootParticle;
        // public string HitParticle => hitParticle;
        public int MaxAmmo => maxAmmo;
        public RuntimeAnimatorController ReloadAnimator => reloadAnimator;
        public List<AnimationsReloads> AnimationsReloads => animationsReloads;

        #endregion

        
    }

}

[Serializable]
public class AnimationsReloads
{
    public string nameAnimation;
    public AnimationClip animationClip;
}

public enum WeaponType 
{
    None, 
    Pistol, 
    AutomaticRifle,
    Shotgun,
    SniperRifle,
}
