using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponStoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text CurrentCurrencyText;
    [SerializeField] private WeaponStoreManager WeaponStoreManager;
    
    void Start()
    {
        CurrencyManager.Instance.InitializeStore();
        CurrentCurrencyText.text = CurrencyManager.Instance.CurrentCurrency.ToString();
    }

   public void BuyWeapon(WeaponData weapon)
   {
       WeaponStoreManager.BuyWeapon(weapon.ID);
   }
    
}
