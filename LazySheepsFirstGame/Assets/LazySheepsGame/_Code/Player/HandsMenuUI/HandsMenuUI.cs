using com.LazyGames;
using com.LazyGames.Dio;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class HandsMenuUI : MonoBehaviour
{
    
    [Header("Player Manager")]
    [SerializeField] private PlayerManager _playerManager;
    
    [Header("UI")]
    [SerializeField] private GameObject _runButton;
    [SerializeField] private Button exitButton;
    
    [Header("Health")]
    [SerializeField] private IntEventChannelSO onUpdatePlayerHealth;
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] Slider _playerHealthSlider;
    
    [Header("Currency")]
    [SerializeField] private IntEventChannelSO onUpdatePlayerCurrency;
    [SerializeField] private TMP_Text _playerCurrencyText;
    
    [Header("Objective")]
    [SerializeField] private TMP_Text _objectiveText;
    
    [Header("Scenes Channel")]
    [SerializeField] private GenericDataEventChannelSO _changeSceneChannel;
    [Scene]
    [SerializeField] private string _sceneRun;
    [Scene]
    [SerializeField] private string _sceneTabern;
    [Scene]
    [SerializeField] private string _sceneAI;

    void Start()
    {
        onUpdatePlayerHealth.IntEvent += UpdatePlayerHealth;
        onUpdatePlayerCurrency.IntEvent += UpdatePlayerCurrency;
        
        exitButton.gameObject.SetActive(false);
        
        _playerManager = PlayerManager.Instance;
        _playerManager.OnSetObjective += SetObjective;
        SetLifeMaxValue(_playerManager.MaxHealth);

    }
    
    public void OnClickWeapon(WeaponData weaponData)
    {
        PlayerManager.Instance.SelectWeaponPlayerHolster(weaponData.ID);
    }

    [Button]
    public void OnClickRun()
    {
        _runButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);

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

    private void SetObjective(Objectives objective)
    {
        _objectiveText.text = objective.Objective;
    }
    private void SetLifeMaxValue(int value)
    {
        _playerHealthSlider.maxValue = value;
        _playerHealthSlider.value = value;
    }
    
    private void UpdatePlayerHealth(int value)
    {
        _playerHealthText.text = value.ToString();
        _playerHealthSlider.value = value;
    }
    private void UpdatePlayerCurrency(int value)
    {
        _playerCurrencyText.text = value.ToString();
    }
}
