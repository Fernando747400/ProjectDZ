using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModuleData", menuName = "LazySheeps/Level Generation", order = 1)]
public class ModuleData : ScriptableObject
{
    [SerializeField] GameObject modulePrefab;
    [SerializeField] Vector2 moduleSize;
    
    // [SerializeField] GameObject[] 
}
