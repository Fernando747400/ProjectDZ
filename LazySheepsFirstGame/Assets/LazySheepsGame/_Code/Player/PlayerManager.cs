using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using com.LazyGames.DZ;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

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
    [SerializeField] private Transform holsterWeapons;
    
    [Header("Holster")]
    [SerializeField] private XRSocketInteractor socketInteractorWeapon;
    
    [Header("Weapon ID Channel")]
    [SerializeField] private GenericDataEventChannelSO weaponSelectChannel;
    
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

    private void OnDisable()
    {
        weaponSelectChannel.StringEvent -= SelectWeapon;
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
        weaponSelectChannel.StringEvent += SelectWeapon;
    }
    #endregion


    #region Public Methods
    public GameObject GetWeaponObject(string weaponID)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.WeaponData.ID == weaponID)
            {
                if(weapon.gameObject.activeSelf == false)
                    weapon.gameObject.SetActive(true);
                
                return weapon.gameObject;
            }
        }

        return null;
    }
    public void SelectWeapon(string weaponID)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.WeaponData.ID == weaponID)
            {
                weapon.gameObject.SetActive(true);
                weapon.EnableGrabInteractable(true);
                weapon.gameObject.transform.position = holsterWeapons.position;

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
    public void DisableAllWeapons()
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
            weapon.EnableGrabInteractable(false);
        }
    }
    public void ResetPlayersPosition()
    {
        transform.parent.position = Vector3.zero;
    }

    #endregion

    public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
    {
        
    }
}
