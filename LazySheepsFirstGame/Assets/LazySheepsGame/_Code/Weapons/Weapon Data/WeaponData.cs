using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData", order = 0)]
    public class WeaponData : AgressorData
    {
        [Header("Weapon Data")] 
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float maxDistance;
        [SerializeField] private GameObject shootParticle;
        [SerializeField] private GameObject hitParticle;
        [SerializeField] private float cooldownPerShot;
        [SerializeField] private float delayReload;
        
        
        #region public variables
        public float BulletSpeed
        {
            get => bulletSpeed;
        }

        public float MaxDistance
        {
            get => maxDistance;
        }

        public GameObject ShootParticle => shootParticle;
        
        public GameObject HitParticle => hitParticle;

        #endregion
        
    }
}