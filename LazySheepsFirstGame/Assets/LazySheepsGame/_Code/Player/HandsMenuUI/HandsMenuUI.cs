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

    [SerializeField] private WeaponStoreManager _weaponStoreManager;
    
    [Header("UI")]
    [SerializeField] private GameObject _runButton;
    [SerializeField] private GameObject exitButton;
    
    [Header("Health")]
    [SerializeField] private IntEventChannelSO onUpdatePlayerHealth;
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] Slider _playerHealthSlider;
    
    [Header("Currency")]
    [SerializeField] private IntEventChannelSO onUpdatePlayerCurrency;
    [SerializeField] private TMP_Text _playerCurrencyText;
    
    [Header("Objective")]
    [SerializeField] private TMP_Text _objectiveText;
    [SerializeField] private GenericDataEventChannelSO onObjectiveCompletedChannel;

    
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
        _runButton.gameObject.SetActive(false);
        
        if(_playerManager == null) _playerManager = PlayerManager.Instance;
        
        _playerManager.OnSetObjective += SetObjective;
        SetObjective(_playerManager.CurrentObjective);
        onObjectiveCompletedChannel.StringEvent += OnCompletedObjective;
        _playerCurrencyText.text = CurrencyManager.Instance.CurrentCurrency.ToString();
        _playerHealthText.text = _playerManager.CurrentHealth.ToString();
        SetLifeValue(_playerManager.CurrentHealth);

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
        _weaponStoreManager.EnableStore(false);

        _playerManager.ResetPlayersPosition(new Vector3(0,1.5f,0));
        
        
        _changeSceneChannel.RaiseStringEvent(_sceneRun);
        _changeSceneChannel.RaiseStringEvent(_sceneAI);
        _changeSceneChannel.RaiseBoolEvent(true);
        
        onObjectiveCompletedChannel.RaiseStringEvent("Run");
    }

    [Button]
    public void OnClickReturn()
    {
        _runButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        _weaponStoreManager.EnableStore(true);
        
        _playerManager.ResetPlayersPosition(new Vector3(0,1.5f,0));
        
        _changeSceneChannel.RaiseStringEvent(_sceneTabern);
        _changeSceneChannel.RaiseBoolEvent(true);  
        
        // onObjectiveCompletedChannel.RaiseStringEvent("EnemyCore");

    }

    private void OnCompletedObjective(string objective)
    {
        if (objective == "Presentation")
        {
            _runButton.gameObject.SetActive(true);
        }
    }
    private void SetObjective(Objectives objective)
    {
        _objectiveText.text = objective.Objective;
    }
    private void SetLifeValue(int value)
    {
        _playerHealthSlider.maxValue = PlayerManager.Instance.MaxHealth;
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
