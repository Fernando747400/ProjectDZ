using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HandsMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _handsMenuUI;
    [SerializeField] private CanvasGroup _handsMenuCanvasGroup;
    [SerializeField] private float _fadeDuration = 0.5f;
    void Start()
    {
        
    }
    
    public void OnClickWeapon(WeaponData weaponData)
    {
        PlayerManager.Instance.SelectWeapon(weaponData.ID);
    }
   

    public void DoFadeOut()
    {
        _handsMenuCanvasGroup.DOFade(_fadeDuration, 0f).OnComplete(
            () => _handsMenuUI.SetActive(false)
            );
    }
    public void DoFadeIn()
    {
        _handsMenuUI.SetActive(true);
        _handsMenuCanvasGroup.DOFade(_fadeDuration, 1f);
    }
    
}
