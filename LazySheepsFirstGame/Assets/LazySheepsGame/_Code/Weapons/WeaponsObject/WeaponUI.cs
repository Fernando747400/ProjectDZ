using System.Collections;
using System.Collections.Generic;
using com.LazyGames.DZ;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Color colorNeedReload;
    
    void Start()
    {
        // _weaponObject = GetComponent<WeaponObject>();
    }
    
    public void UpdateTextMMO(int mmo)
    {
        ammoText.text = mmo.ToString();
    }
    
    public void NeedReload(bool value)
    {
        if (value)
        {
            ammoText.color = colorNeedReload;
            
        }
        else
        {
            ammoText.color = Color.white;
        }
    }
}
