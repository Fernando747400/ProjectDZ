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
        
        private List<GameObject>_spawnPoints = new List<GameObject>();
        public List<GameObject> SpawnPoints => _spawnPoints;
        public int SelectedRandomPoint { get; set; }

        #region private variables

        

        #endregion

        #region Unity Methods
        void Start()
        {
            
        }
        #endregion
        

        #region public Methods

        public void Initialize()
        {
            for (int i = 0; i < containerSpawnPoints.transform.childCount; i++)
            {
                _spawnPoints.Add(containerSpawnPoints.transform.GetChild(i).gameObject);
            }
        }

        public Vector3 GetPositionPivot()
        {
            Vector3 positionPivot = _spawnPoints[0].transform.position - transform.position;
            return positionPivot;
        }
        public Vector3 GetRandomPosition()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Count);
            SelectedRandomPoint = randomIndex;
            
            Vector3 randomPosition = _spawnPoints[randomIndex].transform.position - transform.position;
            _spawnPoints.RemoveAt(randomIndex);
            return randomPosition;
        }
        
      
        
        

        #endregion
        
    }
}