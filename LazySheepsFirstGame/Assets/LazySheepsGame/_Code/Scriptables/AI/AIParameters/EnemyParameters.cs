// Creado Raymundo Mosqueda 028/09/23
using UnityEngine;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/AIParameters/EnemyParameters")]
    public class EnemyParameters : ScriptableObject
    {
        public float maxHp = 20f;
        public float enemySpeed = 1f;
        public float detectionRange = 20f;
        public float attackPower = 1f;
        public float attackRange = .5f;
    }
}
