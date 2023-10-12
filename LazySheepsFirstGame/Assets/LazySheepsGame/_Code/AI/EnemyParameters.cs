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
        
        [Header("Wander Variables")]
        [Tooltip("Radius of the circle, also its distance from the npc")]
        public float circleRadius = 3f;
        [Tooltip("The range that the angle cam move along the circles' diameter")]
        public float deviationRange = 3f;
        [Tooltip("Bottom range of the time the npc remains still or moving")]
        public float minActTime = 5f;
        [Tooltip("Top range of the time the npc remains still or moving")]
        public float maxActTime = 20f;
    }
}
