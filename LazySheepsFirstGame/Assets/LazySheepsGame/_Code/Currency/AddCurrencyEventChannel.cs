using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AddCurrencyEventChannel", menuName = "ScriptableObject/Events/AddCurrencyEventChannel")]

public class AddCurrencyEventChannel : ScriptableObject
{
    public UnityAction<CurrencyData> AddCurrencyEvent;
    public void RaiseAddCurrencyEvent(CurrencyData currencyData)
    {
        AddCurrencyEvent?.Invoke(currencyData);
    }
}
