using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "RemoveCurrencyEventChannel", menuName = "ScriptableObject/Events/RemoveCurrencyEventChannel")]
public class RemoveCurrencyEventChannel : ScriptableObject
{
   public UnityAction<CurrencyData> RemoveCurrencyEvent;
   public void RaiseRemoveCurrencyEvent(CurrencyData currencyData)
   {
      RemoveCurrencyEvent?.Invoke(currencyData);
   }
}
