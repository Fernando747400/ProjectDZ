using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    [CreateAssetMenu(fileName = "ModuleData", menuName = "LazySheeps/Level Generation", order = 1)]
    public class ModuleData : ScriptableObject
    {
        [SerializeField] GameObject modulePrefab;
        [SerializeField] Vector2 moduleSize;
        [SerializeField] int connectionPoints;
        [SerializeField] ModuleType moduleType;
        
        public int ConnectionPoints => connectionPoints;


    }

    public enum ModuleType
    {
        None,
        Room,
        Corridor
        
    }
}