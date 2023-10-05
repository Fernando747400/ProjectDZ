// Creado Raymundo Mosqueda 028/09/23
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/AIParameters/EnemyParameters")]
    public class EnemyParameters : ScriptableObject
    {
        public float maxHp = 20f;
        public float baseSpeed = 1f;
        public float alertSpeed = 2f;
        public float aggroSpeed = 3f;
        public float softDetectionRange = 20f;
        public float hardDetectionRange = 7f;
        public float attackPower = 1f;
        public float attackRange = .5f;
    }
}
