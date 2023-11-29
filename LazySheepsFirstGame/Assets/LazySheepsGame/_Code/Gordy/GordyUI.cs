using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Obvious.Soap;

public class GordyUI : MonoBehaviour
{
    [Header("Dependencies")]
    [Required]
    [SerializeField] private Slider _manaBar;
    [Required]
    [SerializeField] private FloatVariable _mana;
    [Required]
    [SerializeField] private FloatVariable _maxMana;

    private void OnEnable()
    {
        _mana.OnValueChanged += UpdateHealth;
        _maxMana.OnValueChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _mana.OnValueChanged -= UpdateHealth;
        _maxMana.OnValueChanged -= UpdateHealth;
    }

    private void UpdateHealth(float value)
    {
        _manaBar.value = _mana/_maxMana;
    }
}
