using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using com.LazyGames;
using com.LazyGames.Dio;
using com.LazyGames.DZ;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponStoreManager : MonoBehaviour
{
    [SerializeField] private GenericDataEventChannelSO weaponSelectPlayerChannel;
    [SerializeField] private List<WeaponData> weaponsData;
    [SerializeField] private Transform weaponShowPosition;
    [SerializeField] private GenericDataEventChannelSO onGrabWeaponFromStoreChannel;
    [SerializeField] private PlacePoint placePoint;
    
    [SerializeField] private List<WeaponButtonUI> weaponButtons;
    
    void Start()
    {
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Buy Input".SetColor("#96E542"));
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
                PlayerManager.Instance.CleanPlayerHolster();
                PlayerManager.Instance.DisableAllWeapons();
                
                GameObject o = PlayerManager.Instance.GetWeaponObject(weaponData.ID);
                o.transform.position = weaponShowPosition.position;
                o.transform.rotation = weaponShowPosition.rotation;
                
                WeaponObject weaponObject = o.GetComponent<WeaponObject>();
                weaponObject.EnableGrabInteractable(true);
                weaponObject.EnableWeaponStorePart(true);
                weaponObject.InitializeWeapon();
                weaponObject.IsInStore = true;
                
                placePoint.forcePlace = true;
                placePoint.TryPlace(weaponObject.AutoHandGrabbable);
                placePoint.Place(weaponObject.AutoHandGrabbable);
                
                DoWeaponRotation(o);
                
                onGrabWeaponFromStoreChannel.StringEvent += OnGrabWeapon;
                
                DisableButton(weaponData);
                
                Debug.Log("Buy Weapon: ".SetColor("#96E542") + weaponObject.WeaponData.ID);
            }
            
        }
    }
    
    public void SelectWeapon(string weaponID)
    {
        weaponSelectPlayerChannel.RaiseStringEvent(weaponID);
    }

    private void DoWeaponRotation(GameObject weapon)
    {
        float rotationSpeed = 360f; 
        Vector3 rotation = new Vector3(0, rotationSpeed, 0);
        float duration = 5f; 
        weapon.transform.DORotate(rotation, duration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    private void DisableButton(WeaponData weaponData)
    {
        foreach (var button in weaponButtons)
        {
            if (button.WeaponData.ID == weaponData.ID)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
    private void OnGrabWeapon(string weaponID)
    {
        var weaponData = weaponsData.Find(x => x.ID == weaponID);
        if (weaponData != null)
        {
            GameObject o = PlayerManager.Instance.GetWeaponObject(weaponData.ID);
            // WeaponObject weaponObject = o.GetComponent<WeaponObject>();
            // weaponObject.Rigidbody.isKinematic = false;
            // weaponObject.EnableGrabInteractable(true);
            o.transform.DOKill();
            // Debug.Log("OnGrabWeapon: ".SetColor("#96E542") + weaponID);
        }

        
    }
    
}
