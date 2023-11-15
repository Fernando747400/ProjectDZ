using System;
using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine;

public class WeaponStoreManager : MonoBehaviour
{
    [SerializeField] private GenericDataEventChannelSO weaponSelectChannel;
    [SerializeField] private List<WeaponData> weaponsData;
    // [SerializeField] private List<GameObject> weaponsVisuals;
    [SerializeField] private Transform weaponShowPosition;
    
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuyWeapon(weaponsData[1].ID);
        }
    }

    public void BuyWeapon(string weaponID)
    {
        var weaponData = weaponsData.Find(x => x.ID == weaponID);
        if (weaponData != null)
        {
            if (CurrencyManager.Instance.TryBuy(weaponData.CurrencyData))
            {
                GameObject weapon = PlayerManager.Instance.GetWeaponObject(weaponData.ID);
                PlayerManager.Instance.DisableAllWeapons();
                weapon.transform.position = weaponShowPosition.position;
                
                // weaponSelectChannel.RaiseStringEvent(weaponID);
            }
            
        }
    }
    
    public void SelectWeapon(string weaponID)
    {
        weaponSelectChannel.RaiseStringEvent(weaponID);
    }
}
