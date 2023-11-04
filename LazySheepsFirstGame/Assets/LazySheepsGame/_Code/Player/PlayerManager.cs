using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.DZ;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGeneralTarget
{
    private static PlayerManager _instance;
    
    public static PlayerManager Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();
                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PlayerManager>();
                    singletonObject.name = typeof(PlayerManager).ToString();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
     
    }

    #region Serialized Fields


    [Header("Right Hand")]
    [SerializeField] private Transform rightHandAttachPoint;
    
    [Header("Left Hand")]
    [SerializeField] private Transform leftHandAttachPoint;

    [Header("Weapons")] 
    [SerializeField] private WeaponData currentWeaponData;
    [SerializeField] private List<WeaponObject> weapons;
    
    #endregion


    #region public Variables

    public Transform RightHandAttachPoint => rightHandAttachPoint;
    public Transform LeftHandAttachPoint => leftHandAttachPoint;
    #endregion

    #region Unity Methods
    private void Awake()
    {
       CreateInstance();
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
        currentWeaponData = weapons[0].WeaponData;
        SelectWeapon(currentWeaponData.ID);   
    }
    #endregion


    #region Public Methods
    public void SelectWeapon(string weaponID)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.WeaponData.ID == weaponID)
            {
                weapon.gameObject.SetActive(true);
                weapon.InitializeWeapon();
                currentWeaponData = weapon.WeaponData;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
        
    }
    

    #endregion

    public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
    {
        
    }
}
