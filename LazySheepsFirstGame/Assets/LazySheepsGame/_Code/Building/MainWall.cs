using com.LazyGames;
using com.LazyGames.Dio;
using NaughtyAttributes;
using UnityEngine;

public class MainWall : MonoBehaviour, IGeneralTarget
{
    [Header("Dependencies")]

    [Required]
    [SerializeField] private WallHealthBar _healthBar;

    [Header("Scriptable Object Channels")]

    [Required]
    [SerializeField] private GameObjectEventChannelSO _wallDestroyedChannel;

    [Header("Settings")]
    [SerializeField] private float _maxHealth = 100;
    
    private float _health = 100;

    private void Start()
    {
        Prepare();
    }

    public void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0)
    {
        _health -= dmg;
        CheckHealth();
        UpdateHealth();
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _wallDestroyedChannel.RaiseEvent(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void UpdateHealth()
    {
        _healthBar.UpdateHealth(_health);
    }

    private void Prepare()
    {
        _healthBar.MaxHealth = _maxHealth;
        _healthBar.UpdateHealth(_health);
    }
}
