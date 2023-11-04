using System.Collections;
using System.Collections.Generic;
using com.LazyGames;
using com.LazyGames.Dio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HandsMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _handsMenuUI;
    [SerializeField] private Button _runButton;
    [SerializeField] private GenericDataEventChannelSO changeSceneChannel;
    [SerializeField] private string sceneRun = "VerticalSlice_0.1";
    [SerializeField] private string sceneTabern = "Tabern";
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
