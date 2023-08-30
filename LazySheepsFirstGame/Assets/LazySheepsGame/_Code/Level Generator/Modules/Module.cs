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
            GetSpawnPointsfromContainer();
        }
        #endregion

        #region Private Methods
        private void GetSpawnPointsfromContainer()
        {
            _spawnPoints = new GameObject[containerSpawnPoints.transform.childCount];
            for (int i = 0; i < containerSpawnPoints.transform.childCount; i++)
            {
                _spawnPoints[i] = containerSpawnPoints.transform.GetChild(i).gameObject;
            }
            Debug.Log("SpawnPoints: " + _spawnPoints.Length);
        }

        

        #endregion

        
        
    }
}