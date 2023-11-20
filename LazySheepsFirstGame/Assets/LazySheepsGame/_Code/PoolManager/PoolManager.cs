using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace com.LazyGames.DZ
{
    public class PoolManager : ManagerBase
    {
        [SerializeField] private PoolManagerData poolManagerData;

        private static PoolManager _instance;

        public static PoolManager Instance
        {
            get
            {
                // if (_instance != null) return _instance;

                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PoolManager>();
                    singletonObject.name = "PoolManager";
                    _instance.poolManagerData = Resources.Load("PoolManagerData") as PoolManagerData;
                    _instance.InitManager();
                    
                    DontDestroyOnLoad(singletonObject);

                }
                
                return _instance;
            }
        }
        

        #region private Methods
        
        private void CreateInstance()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }
        
        #endregion

        public GameObject SpawnPool(string pool)
        {
           GameObject leanPoolGameObject = LeanPool.Spawn(GetPrefab(pool));
           
           return leanPoolGameObject;
        }
        
        public GameObject GetPrefab(string poolID)
        {
            foreach (var pool in poolManagerData.Pools)
            {
                if (pool.poolID == poolID)
                {
                    return pool.poolPrefab;
                }
            }

            return null;
        }


        #region ManagerBase

        public override void InitManager()
        {
            if (FinishedLoading) return;
            CreateInstance();
            FinishedLoading = true;
            FinishLoading?.Invoke();
            Debug.Log("PoolManager Initialized");
                
        }

        #endregion
        
    }
    
    
    

    [Serializable]
    public class Pools
    {
        public string poolID;
        public GameObject poolPrefab;
    }
}

