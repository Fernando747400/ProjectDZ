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
        [SerializeField] private string hitParticle;
        [SerializeField] private float cooldownPerShot;
        [SerializeField] private float delayReload;
        [SerializeField] private int maxAmmo;
        [SerializeField] private RuntimeAnimatorController reloadAnimator;
        


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

        public GameObject ShootParticle => shootParticle;

        public string HitParticle => hitParticle;
        public int MaxAmmo => maxAmmo;

        #endregion

    }

}

public enum WeaponType 
{
    None, 
    Pistol, 
    AutomaticRifle,
    Shotgun,
    SniperRifle,
}
