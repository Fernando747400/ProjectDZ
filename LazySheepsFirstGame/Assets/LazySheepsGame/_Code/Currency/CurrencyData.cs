using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "CurrencyData", order = 0)]
    public class CurrencyData : ScriptableObject
    {
      [SerializeField] private int initialCurrency;
      [SerializeField] private CurrencyType currencyType;
      
      public int InitialCurrency => initialCurrency;
      public CurrencyType CurrencyType => currencyType;
    }
}