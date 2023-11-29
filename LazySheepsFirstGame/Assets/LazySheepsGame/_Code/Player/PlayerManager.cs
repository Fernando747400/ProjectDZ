using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using com.LazyGames;
using com.LazyGames.Dio;
using com.LazyGames.DZ;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : ManagerBase, IGeneralTarget
{
    private static PlayerManager _instance;
    
    public static PlayerManager Instance
    {

        get
        {

            if (_instance != null) return _instance;
             
            var singletonObject = new GameObject(); 
            _instance = singletonObject.AddComponent<PlayerManager>(); 
            singletonObject.name = typeof(PlayerManager).ToString(); 
            DontDestroyOnLoad(singletonObject);
            
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();
                
            }

            return _instance;
        }
     
    }

    #region Serialized Fields
    

    [Header("Player")]
    [SerializeField] int playerHealth = 100;
    [SerializeField] private GameObject player;
    
    [Header("Weapons")] 
    [SerializeField] private WeaponData currentWeaponData;
    [SerializeField] private List<WeaponObject> weapons;
    [SerializeField] private Transform playerHolsterWeapon;
    
    [Header("Weapon ID Channel")]
    [SerializeField] private GenericDataEventChannelSO weaponSelectChannel;
    
    [Header("Currency")]
    [SerializeField] private IntEventChannelSO onDeathEnemyChannel;
    [SerializeField] private AddCurrencyEventChannel addCurrencyEventChannel;
    
    [Header("Heal")]
    [SerializeField] private IntEventChannelSO onHealPlayerChannel;
    [SerializeField] private IntEventChannelSO onUpdateHealthPlayerChannel;
    
    [Header("Objectives")]
    [SerializeField] private GenericDataEventChannelSO onObjectiveCompletedChannel;
    [SerializeField] private ScriptableEventObjectives onSetObjectivesSO;

    [SerializeField] ObjectivesData objectivesData;
    
    [Header("PlacePoint")]
    [SerializeField] private PlacePoint placeGunPointHolster;
    
    
    #endregion

    #region private Variables
    private int _currentHealth;
    private Objectives _currentObjective;
    
    public int MaxHealth => playerHealth;
    // public event Action<Objectives> OnSetObjective;
    public Objectives CurrentObjective => _currentObjective;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            onUpdateHealthPlayerChannel.RaiseEvent(_currentHealth);
        }
    }
    

    #endregion

    #region public Variables

    #endregion

    #region Unity Methods
   
    private void OnDisable()
    {
        weaponSelectChannel.StringEvent -= SelectWeaponPlayerHolster;
    }

    void Start()
    {
        InitializePlayer();
    }
    #endregion

    #region private Methods
    private void CreateInstance()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    
    private void InitializePlayer()
    {
        _currentHealth = playerHealth;
        weaponSelectChannel.StringEvent += SelectWeaponPlayerHolster;
        onObjectiveCompletedChannel.StringEvent += OnCompletedObjective;
        onDeathEnemyChannel.IntEvent += OnKilledEnemy;
        onHealPlayerChannel.IntEvent += HealPlayer;
        
        SetObjective("Presentation");
        objectivesData.ResetObjectives();
        
        currentWeaponData = weapons[0].WeaponData;
        SelectWeaponPlayerHolster(currentWeaponData.ID); 
        
    }
    #endregion


    #region Public Methods
    public GameObject GetWeaponObject(string weaponID)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.WeaponData.ID == weaponID)
            {
                // if(weapon.gameObject.activeSelf == false)
                    weapon.gameObject.SetActive(true);
                
                return weapon.gameObject;
            }
        }

        return null;
    }
    public void SelectWeaponPlayerHolster(string weaponID)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.WeaponData.ID == weaponID)
            {
                weapon.gameObject.SetActive(true);
                weapon.EnableGrabInteractable(true);
                
                if(playerHolsterWeapon != null) weapon.gameObject.transform.position = playerHolsterWeapon.position;

                placeGunPointHolster.forcePlace = true;
                placeGunPointHolster.TryPlace(weapon.AutoHandGrabbable);
                placeGunPointHolster.Place(weapon.AutoHandGrabbable);
                    
                
                weapon.InitializeWeapon();
                currentWeaponData = weapon.WeaponData;
                
                Debug.Log("Select Weapon: ".SetColor("#87E720") + weaponID);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                weapon.EnableGrabInteractable(false);
            }
        }
        
    }

    public void CleanPlayerHolster()
    {
        Grabbable grabbableToRemove = GetWeaponObject(currentWeaponData.ID).GetComponent<Grabbable>();
        placeGunPointHolster.Remove(grabbableToRemove);
    }
    public void DisableAllWeapons()
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
            weapon.EnableGrabInteractable(false);
        }
    }
    public void EnableWeapon(string weaponID)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.WeaponData.ID == weaponID)
            {
                weapon.gameObject.SetActive(true);
                weapon.EnableGrabInteractable(true);
                
                Debug.Log("Enable Weapon: ".SetColor("#87E720") + weaponID);
            }
        }
    }
    public void ResetPlayersPosition(Vector3 position)
    {
        player.transform.position = position;
        // Debug.Log("Reset Player Position".SetColor("#87E720"));
    }

    public Objectives GetObjective(string objectiveID)
    {
        return objectivesData.Objectives.Find(x => x.ID == objectiveID);
    }
    #endregion

    #region private Methods
    
    public void OnCompletedObjective(string objectiveID)
    {
        Objectives objective = objectivesData.Objectives.Find(x => x.ID == objectiveID);
        if(objective.IsCompleted) return;
        
        objective.IsCompleted = true;
        int index = objectivesData.Objectives.FindIndex(x => x.ID == objectiveID);
        Debug.Log("Completed Objective: ".SetColor("#29DDEF") + objectiveID);

        if(index + 1 > objectivesData.Objectives.Count) return;
        
        Objectives nextObjective = objectivesData.Objectives[index + 1];
        SetObjective(nextObjective.ID);
        Debug.Log("Next Objective: ".SetColor("#29DDEF") + nextObjective.Objective);
    }
    
    private void SetObjective(string objectiveID)
    {
        _currentObjective = objectivesData.Objectives.Find(x => x.ID == objectiveID);
        Debug.Log("Set Objective: ".SetColor("#29DDEF") + _currentObjective.Objective);
        
        onSetObjectivesSO.Raise(_currentObjective);
        
    }
    private void HealPlayer(int heal)
    {
        if (_currentHealth < MaxHealth)
        {
            CurrentHealth += heal;
            Debug.Log("Heal Player".SetColor("#87E720") + heal);
        }
        else
        {
            CurrentHealth = MaxHealth;
            Debug.Log("Set max health".SetColor("#87E720"));
        }
        
       
    }

    private void OnKilledEnemy(int currency)
    {
        //Add if player needs to do something after killing enemy
        CurrencyData enemyCurrencyData = new CurrencyData();
        enemyCurrencyData.ValueCurrency = currency;
        addCurrencyEventChannel.RaiseAddCurrencyEvent(enemyCurrencyData);
        
    }
    

    #endregion
    
    #region IGeneralTarget

    public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
    {
        CurrentHealth -= (int) dmg;
        if (_currentHealth <= 0)
        {
            Debug.Log("Player Dead".SetColor("#F51858"));
        }
        
        Debug.Log("Player Received damage :" + dmg);

    }
    #endregion


    #region Manager Base
    public override void InitManager()
    {
        if (FinishedLoading) return;
        CreateInstance();
        FinishedLoading = true;
        FinishLoading?.Invoke();
        
        Debug.Log("PlayerManager Initialized");
        
    }

    #endregion
    

}
