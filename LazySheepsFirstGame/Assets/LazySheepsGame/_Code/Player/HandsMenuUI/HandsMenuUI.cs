using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using NaughtyAttributes;

public class HandsMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _handsMenuUI;
    [SerializeField] private Button _runButton;
    [SerializeField] private GenericDataEventChannelSO changeSceneChannel;

    [Scene]
    [SerializeField] private string sceneRun;
    [Scene]
    [SerializeField] private string sceneTabern;
    void Start()
    {
        
    }
    
    public void OnClickWeapon(WeaponData weaponData)
    {
        PlayerManager.Instance.SelectWeapon(weaponData.ID);
    }
   
    public void OnClickRun()
    {
        _runButton.gameObject.SetActive(false);
        PlayerManager.Instance.ResetPlayersPosition();
        changeSceneChannel.RaiseStringEvent(sceneRun);
        changeSceneChannel.RaiseBoolEvent(true);    
    }
    
    public void OnClickReturn()
    {
        _runButton.gameObject.SetActive(true);
        PlayerManager.Instance.ResetPlayersPosition();
        changeSceneChannel.RaiseStringEvent(sceneTabern);
        changeSceneChannel.RaiseBoolEvent(true);        
    }
    
}
