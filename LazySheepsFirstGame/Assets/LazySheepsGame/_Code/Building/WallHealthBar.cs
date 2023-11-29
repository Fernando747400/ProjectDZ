using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class WallHealthBar : MonoBehaviour
{
    [Header("Dependencies")]

    [Required]
    [SerializeField] private Slider _healthBar;
    [Required]
    [SerializeField] private Image _fillImage;

    [Header("Settings")]
    [SerializeField] private Gradient _colorGradient;

    private float _maxHealth = 100;
    private float _health = 100;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }


    public void UpdateHealth(float health)
    {
        _health = Mathf.Clamp(health, 0, _maxHealth);
        _healthBar.value = health/_maxHealth;
        UpdateColor();
    }

    private void UpdateColor()
    {
        _fillImage.color = _colorGradient.Evaluate(_health / _maxHealth);
    }

    private void OnValidate()
    {
        _healthBar.value = _health / _maxHealth;
        UpdateColor();
    }
}
