using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    [CreateAssetMenu(fileName = "ModuleData", menuName = "LazySheeps/Level Generation", order = 1)]
    public class ModuleData : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] ModuleType moduleType;
        
        public ModuleType ModuleType => moduleType;
        public string MyId => id;
        


    }

    public enum ModuleType
    {
        None,
        Room,
        Corridor
        
    }
}