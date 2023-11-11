using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private CurrencyData currencyData;
    [SerializeField] private int currentCurrency;
    
    public static CurrencyManager Instance;
    public int CurrentCurrency
    {
        get
        {
            return currentCurrency;
        }
        set
        {
            currentCurrency = value;

        }
    }


    private void OnDisable()
    {
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentCurrency = currencyData.InitialCurrency;
    }
    
   public void AddCurrency(int currency)
   {
       currentCurrency += currency; 
   }
   public void RemoveCurrency(int currency)
   {
       currentCurrency -= currency;
   }
   
   public bool TryBuy(int price)
   { 
       if (currentCurrency >= price)
       {
           RemoveCurrency(price);
           return true;
       } 
       return false;
   }
   
   
   
}

