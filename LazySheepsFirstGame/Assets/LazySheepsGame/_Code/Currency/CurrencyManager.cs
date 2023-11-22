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
    [SerializeField] private IntEventChannelSO onUpdateCurrencyChannel;
    
    [SerializeField] private int initialCurrency = 5000;
    
    private int _currentCurrency;
    private AddCurrencyEventChannel AddCurrencyChannel => addCurrencyEventChannel;
    public static CurrencyManager Instance
    {
        get
        {
            
            if (_instance != null) return _instance;
            
            var singletonObject = new GameObject(); 
            _instance = singletonObject.AddComponent<CurrencyManager>(); 
            singletonObject.name = typeof(CurrencyManager).ToString(); 
            _instance.addCurrencyEventChannel = Resources.Load<AddCurrencyEventChannel>("LazySheepsGame/_Scriptables/Currency/AddCurrencyEventChannel");
            _instance.removeCurrencyEventChannel = Resources.Load<RemoveCurrencyEventChannel>("LazySheepsGame/_Scriptables/Currency/RemoveCurrencyEventChannel");
            _instance.InitManager();
            DontDestroyOnLoad(singletonObject);

            return _instance;
           
        }
    }
    public int CurrentCurrency
    {
        get { return _currentCurrency; }

        protected set
        {
            _currentCurrency = value;
            onUpdateCurrencyChannel.RaiseEvent(_currentCurrency);
           
        }
    }

    
    private static CurrencyManager _instance;

    private void OnDisable()
    {
        addCurrencyEventChannel.AddCurrencyEvent -= AddCurrency;
        removeCurrencyEventChannel.RemoveCurrencyEvent -= RemoveCurrency;
    }
    
    private void Start()
    {
        InitializeStore();
    }

    public void InitializeStore()
    {
        addCurrencyEventChannel.AddCurrencyEvent += AddCurrency;
        removeCurrencyEventChannel.RemoveCurrencyEvent += RemoveCurrency;
        
        CurrentCurrency = initialCurrency;
    }
    
   public void AddCurrency(CurrencyData currencyData)
   {
       CurrentCurrency += currencyData.ValueCurrency;
   }
   
   public void RemoveCurrency(CurrencyData currencyData)
   {
       CurrentCurrency -= currencyData.ValueCurrency;
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
   
    public override void InitManager()
    {
        if (FinishedLoading) return;
        CreateInstance();
        FinishedLoading = true;
        FinishLoading?.Invoke();
        
        Debug.Log("CurrencyManager Initialized");
        
    }

   #endregion
}

