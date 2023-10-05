using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames
{
   [CreateAssetMenu(fileName = "AgressorData", menuName = "AgressorData", order = 0)]
   public class AgressorData : ScriptableObject
   {
      [Header("Agressor Data")]
      [SerializeField] private string id;
      [SerializeField] private int damage;
      [SerializeField] private LayerMask layerMasks;

      #region public variables
      public string ID
      {
         get => id;
      }

      public int Damage
      {
         get => damage;
      }

      public LayerMask LayerMasks => layerMasks;
   }
   #endregion

   
   
}