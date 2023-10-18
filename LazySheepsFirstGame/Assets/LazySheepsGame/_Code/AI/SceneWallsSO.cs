// Creado Raymundo Mosqueda 16/10/23

using System.Collections.Generic;
using UnityEngine;
using com.LazyGames.Dio;

namespace com.LazyGames.Dz
{
    [CreateAssetMenu(menuName = "LazySheeps/AI/SceneWallsSO")]
    public class SceneWallsSO : ScriptableObject
    {
        public List<GameObject> Walls => _walls;
        private List<GameObject> _walls;
        

        [SerializeField] private GameObjectEventChannelSO buildEventChannelSo;
        [SerializeField] private GameObjectEventChannelSO destroyEventChannelSo;

        private void OnEnable()
        {
            buildEventChannelSo.GameObjectEvent += AddWall;
            destroyEventChannelSo.GameObjectEvent += RemoveWall;
        }

        private void OnDisable()
        {
            buildEventChannelSo.GameObjectEvent -= AddWall;
            destroyEventChannelSo.GameObjectEvent -= RemoveWall;
        }

        private void AddWall(GameObject wall)
        {
            _walls.Add(wall);
        }

        private void RemoveWall(GameObject wall)
        {
            _walls.Remove(wall);
        }
    }
}