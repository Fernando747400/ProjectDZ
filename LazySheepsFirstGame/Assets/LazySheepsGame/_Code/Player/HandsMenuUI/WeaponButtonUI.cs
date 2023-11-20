using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButtonUI : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private TMP_Text weaponPriceText;
    [SerializeField] private Image WeaponImage;

    

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
        WeaponImage.sprite = weaponData.WeaponSprite;
    }
}
