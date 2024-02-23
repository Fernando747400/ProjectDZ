// Creado Raymundo Mosqueda 28/09/23

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/AI/AiParameters")]
    public class AiParameters : ScriptableObject
    {
        [Header("Economy Variables")] 
        public int killValue = 1;
        
        [Header("Perception Variables")] 
        public bool skittish;
        
        [Header("Movement Variables")]
        public float patrolSpeed = 2f;
        public float baseSpeed = 2f;
        public float alertSpeed = 4f;
        public float aggroSpeed = 6f;
        public float fleeSpeed = 8f;
        
        [Header("Detection Variables")]
        public float softDetectionRange = 20f;
        public float hardDetectionRange = 8f;
        public float oscillationSpeed = 25f; 
        public float coneAngle = 100f; 
        public Vector3 heightOffset = new Vector3(0, .5f, 0);
        [Tooltip("The time in seconds the enemy will look for the source hearing a dangerous noise")]
        public float alertTime = 5f;
        
        [Header("Combat Variables")]
        public float maxHp = 20f;
        public float attackPower = 1f;
        public float attackSpeed = 1.5f;
        public float deAggroDistance = 20f;
        
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
