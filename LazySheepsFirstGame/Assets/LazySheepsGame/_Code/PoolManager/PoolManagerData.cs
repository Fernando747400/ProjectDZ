using System.Collections;
using System.Collections.Generic;
using com.LazyGames.DZ;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolManagerData", menuName = "PoolManagerData", order = 0)]
public class PoolManagerData : ScriptableObject
{
    [SerializeField] private List<Pools> pools;
    
    public List<Pools> Pools => pools;
    
}
