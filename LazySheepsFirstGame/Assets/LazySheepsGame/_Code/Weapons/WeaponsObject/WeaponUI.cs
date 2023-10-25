using System.Collections;
using System.Collections.Generic;
using com.LazyGames.DZ;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Color colorNeedReload;
    [SerializeField] private float strengthShake = 0.5f;
    [SerializeField] private float durationShake = 0.5f;
    [SerializeField] private int vibratoShake = 1;
    [SerializeField] private float randomShake = 90f;
    [SerializeField] private bool snappingShake = false;
    [SerializeField] private bool fadeOutShake = true;
    
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
            Debug.Log("Need Reload");
            ammoText.color = colorNeedReload;
            ammoText.gameObject.transform.DOShakePosition(durationShake, strengthShake, vibratoShake, randomShake, snappingShake, fadeOutShake);

        }
        else
        {
            ammoText.color = Color.white;
        }
    }
}

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(WeaponUI))]
public class WeaponUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WeaponUI weaponUI = (WeaponUI) target;
        if (GUILayout.Button("Need Reload"))
        {
            weaponUI.NeedReload(true);
        }
    }
}

#endif