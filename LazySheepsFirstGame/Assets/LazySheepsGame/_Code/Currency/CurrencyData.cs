using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames
{
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "CurrencyData", order = 0)]
    public class CurrencyData : ScriptableObject
    {
      [SerializeField] private int valueCurrency;
      [SerializeField] private CurrencyType currencyType;

      public int ValueCurrency
      {
          get => valueCurrency;
          set => valueCurrency = value;
      }
      public CurrencyType CurrencyType => currencyType;
    }
}