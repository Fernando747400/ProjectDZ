using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    public class Module : MonoBehaviour
    {
        [SerializeField] private ModuleData moduleData;
        [SerializeField] private GameObject containerSpawnPoints;
        public ModuleData MyData => moduleData;
        
        private GameObject[] _spawnPoints;
        public GameObject[] SpawnPoints => _spawnPoints;

        #region Unity Methods
        void Start()
        {
            _spawnPoints = new GameObject[containerSpawnPoints.transform.childCount];
            Debug.Log("SpawnPoints: " + _spawnPoints.Length);
            // GetSpawnPointsfromContainer();
        }
        #endregion

        #region Private Methods
        private void GetSpawnPointsfromContainer()
        {
           
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                _spawnPoints[i] = containerSpawnPoints.transform.GetChild(i).gameObject;
            }
            Debug.Log("SpawnPoints: " + _spawnPoints.Length);
        }

        

        #endregion

        
        
    }
}