using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine;
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
        PlayerManager.Instance.SelectWeaponPlayerHolster(weaponData.ID);
    }

    [Button]
    public void OnClickRun()
    {
        _runButton.gameObject.SetActive(false);
        PlayerManager.Instance.ResetPlayersPosition();
        _changeSceneChannel.RaiseStringEvent(_sceneRun);
        _changeSceneChannel.RaiseStringEvent(_sceneAI);
        _changeSceneChannel.RaiseBoolEvent(true);
    }

    [Button]
    public void OnClickReturn()
    {
        _runButton.gameObject.SetActive(true);
        PlayerManager.Instance.ResetPlayersPosition();
        _changeSceneChannel.RaiseStringEvent(_sceneTabern);
        _changeSceneChannel.RaiseBoolEvent(true);        
    }
    
}
