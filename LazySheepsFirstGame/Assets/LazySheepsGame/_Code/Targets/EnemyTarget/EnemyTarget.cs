using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;

namespace com.LazyGames
{
    public class EnemyTarget : MonoBehaviour, IGeneralTarget
    {

        [Header("Enemy Target")] 
        [SerializeField] private TargetsData targetsData;

        private string id;
        private int maxHealth;
        private TargetsType type;
        private float currentHp;

        
        #region public variables

        public string ID
        {
            get => id;
            set => id = value;
        }

        public int MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public TargetsType Type
        {
            get => type;
            set => type = value;
        }

        public float CurrentHp
        {
            get => currentHp;
            set => currentHp = value;
        }

        #endregion

        #region Unity methods

        private void Start()
        {
            PrepareTarget();
        }
    

        #endregion
    
        #region private methods
        
        private void PrepareTarget()
        {
            ID = targetsData.ID;
            Type = targetsData.Type;
            MaxHealth = targetsData.MaxHealth;
            CurrentHp = MaxHealth;
        }
        #endregion

        public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
        {
            Debug.Log($"received aggressor");
        }
    }

}