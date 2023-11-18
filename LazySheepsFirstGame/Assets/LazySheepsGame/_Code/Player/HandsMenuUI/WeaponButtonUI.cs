using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using TMPro;
using UnityEngine;

public class WeaponButtonUI : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text weaponPriceText;
    

    public WeaponData WeaponData => weaponData;


    private void Start()
    {
        InitializeButton(WeaponData);
    }

    public void InitializeButton(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        weaponNameText.text = weaponData.ID;
        weaponPriceText.text = weaponData.CurrencyData.ValueCurrency.ToString();
    }
}
