using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
   [CreateAssetMenu(fileName = "AgressorData", menuName = "AgressorData", order = 0)]
   public class AgressorData : ScriptableObject
   {
      [SerializeField] private string id;
      [SerializeField] private int damage;

      #region public variables
      public string ID
      {
         get => id;
      }

      public int Damage
      {
         get => damage;
      }
   }
   #endregion

}