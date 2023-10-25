using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace com.LazyGames.DZ
{
    public class PoolManager : MonoBehaviour
    {
        
        public static PoolManager Instance;
        [SerializeField] private List<Pools> pools;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            
            DontDestroyOnLoad(this);
        }
        
        public GameObject SpawnPool(string pool)
        {
           GameObject leanPoolGameObject = LeanPool.Spawn(GetPrefab(pool));
           
           return leanPoolGameObject;
        }
        
        public GameObject GetPrefab(string poolID)
        {
            foreach (var pool in pools)
            {
                if (pool.poolID == poolID)
                {
                    return pool.poolPrefab;
                }
            }

            return null;
        }

    }

    [Serializable]
    public class Pools
    {
        public string poolID;
        public GameObject poolPrefab;
    }
}

