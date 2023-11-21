using System;
using System.Collections;
using System.Collections.Generic;
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
    
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log("Buy Weapon".SetColor("#96E542"));
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
                PlayerManager.Instance.DisableAllWeapons();
                GameObject o = PlayerManager.Instance.GetWeaponObject(weaponData.ID);
                WeaponObject weaponObject = o.GetComponent<WeaponObject>();
                weaponObject.EnableGrabInteractable(true);
                weaponObject.EnableWeaponStorePart(true);
                weaponObject.InitializeWeapon();
                onGrabWeaponFromStoreChannel.StringEvent += OnGrabWeapon;
                o.transform.position = weaponShowPosition.position;
                o.transform.rotation = weaponShowPosition.rotation;
                
                weaponObject.Rigidbody.isKinematic = true;
                
                DoWeaponRotation(o);
                
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

    private void OnGrabWeapon(string weaponID)
    {
        var weaponData = weaponsData.Find(x => x.ID == weaponID);
        if (weaponData != null)
        {
            GameObject o = PlayerManager.Instance.GetWeaponObject(weaponData.ID);
            WeaponObject weaponObject = o.GetComponent<WeaponObject>();
            // weaponObject.Rigidbody.isKinematic = false;
            // weaponObject.EnableGrabInteractable(true);
            o.transform.DOKill();
            Debug.Log("OnGrabWeapon: ".SetColor("#96E542") + weaponID);
        }

        
    }
    
}
