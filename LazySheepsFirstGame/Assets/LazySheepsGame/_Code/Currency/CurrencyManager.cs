using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine;

public class CurrencyManager : ManagerBase
{
    [Header("Currency")]
    [SerializeField] private AddCurrencyEventChannel addCurrencyEventChannel;
    [SerializeField] private RemoveCurrencyEventChannel removeCurrencyEventChannel;
    
    private int _currentCurrency;
    public static CurrencyManager Instance
    {
        get
        {
            
            if (_instance != null) return _instance;
            
            var singletonObject = new GameObject(); 
            _instance = singletonObject.AddComponent<CurrencyManager>(); 
            singletonObject.name = typeof(CurrencyManager).ToString(); 
            DontDestroyOnLoad(singletonObject);

            return _instance;
           
        }
    }
    public int CurrentCurrency
    {
        get { return _currentCurrency; }
        
        protected set { _currentCurrency = value; }
    }

    
    private static CurrencyManager _instance;

    private void OnDisable()
    {
        addCurrencyEventChannel.AddCurrencyEvent -= AddCurrency;
        removeCurrencyEventChannel.RemoveCurrencyEvent -= RemoveCurrency;
    }
    
    private void Start()
    {
        addCurrencyEventChannel.AddCurrencyEvent += AddCurrency;
        removeCurrencyEventChannel.RemoveCurrencyEvent += RemoveCurrency;
    }
    
   public void AddCurrency(CurrencyData currencyData)
   {
       _currentCurrency += currencyData.ValueCurrency;
   }
   
   public void RemoveCurrency(CurrencyData currencyData)
   {
       _currentCurrency -= currencyData.ValueCurrency;
   }
  
   public bool TryBuy(CurrencyData currencyData)
   {
       switch (currencyData.CurrencyType)
       {
           case CurrencyType.Iron:
               if (_currentCurrency >= currencyData.ValueCurrency)
               {
                   RemoveCurrency(currencyData);
                   return true;
               }
               break;
       }
       // if (_currentCurrency >= currencyData.ValueCurrency)
       // {
       //     RemoveCurrency(currencyData);
       //     return true;
       // }
       
       return false;
   }

   private void CreateInstance()
   {
         if (_instance != null)
         {
              Destroy(gameObject);
              return;
         }
         _instance = this;
   }
   

   #region ManagerBase
   
    public override void Init()
    {
        if (FinishedLoading) return;
        CreateInstance();
        FinishedLoading = true;
        FinishLoading?.Invoke();
        
        Debug.Log("CurrencyManager Initialized");
        
    }

   #endregion
}

