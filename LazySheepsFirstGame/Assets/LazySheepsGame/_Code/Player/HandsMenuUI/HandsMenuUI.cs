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
    [SerializeField] private GenericDataEventChannelSO _changeSceneChannel;

    [Scene]
    [SerializeField] private string _sceneRun;
    [Scene]
    [SerializeField] private string _sceneTabern;
    [Scene]
    [SerializeField] private string _sceneAI;

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
        _changeSceneChannel.RaiseStringEvent(_sceneRun);
        _changeSceneChannel.RaiseStringEvent(_sceneAI);
        _changeSceneChannel.RaiseBoolEvent(true);
    }
    
    public void OnClickReturn()
    {
        _runButton.gameObject.SetActive(true);
        PlayerManager.Instance.ResetPlayersPosition();
        _changeSceneChannel.RaiseStringEvent(_sceneTabern);
        _changeSceneChannel.RaiseBoolEvent(true);        
    }
    
}
