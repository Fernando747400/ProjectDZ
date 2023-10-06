// Creado Raymundo Mosqueda 028/09/23
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/AIParameters/EnemyParameters")]
    public class EnemyParameters : ScriptableObject
    {
        [Header("Movement Variables")]
        public float baseSpeed = 1f;
        public float alertSpeed = 2f;
        public float aggroSpeed = 3f;
        [Header("Detection Variables")]
        public float softDetectionRange = 20f;
        public float hardDetectionRange = 7f;
        public float oscillationSpeed = 50f; 
        public float coneAngle = 45f; 
        public Vector3 heightOffset = new Vector3(0, .5f, 0);
        [Header("Combat Variables")]
        public float maxHp = 20f;
        public float attackPower = 1f;
        public float attackSpeed = 1.5f;
    }
}
