using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponStoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text CurrentCurrencyText;
    [SerializeField] private WeaponStoreManager WeaponStoreManager;
    [SerializeField] private IntEventChannelSO OnUpdateCurrencyChannel;
    
    void Start()
    {
        CurrencyManager.Instance.InitializeStore();
        CurrentCurrencyText.text = CurrencyManager.Instance.CurrentCurrency.ToString();
        OnUpdateCurrencyChannel.IntEvent += OnUpdateCurrency;
    }

   public void BuyWeapon(WeaponData weapon)
   {
       WeaponStoreManager.BuyWeapon(weapon.ID);
   }
   
   void OnUpdateCurrency(int currency)
   {
       CurrentCurrencyText.text = currency.ToString();
   }
   
    
}
