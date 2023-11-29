using UnityEngine;
using Obvious.Soap;
using System.Collections.Generic;
using NaughtyAttributes;

public class GordyKeyColor : MonoBehaviour
{
    [Header("Dependencies")]

    [Required]
    [SerializeField] private FloatVariable _maxMana;
    [Required]
    [SerializeField] private FloatVariable _mana;

    [SerializeField] private List<GameObject> _damageKeys = new List<GameObject>();
    [SerializeField] private List<GameObject> _healthKeys = new List<GameObject>();

    [SerializeField] private List<Material> _damageColors = new List<Material>();
    [SerializeField] private List<Material> _healthColors = new List<Material>();

    [SerializeField] private Material _offMaterial;

    private void OnEnable()
    {
        _mana.OnValueChanged += UpdateKeys;
    }

    private void OnDisable()
    {
        _mana.OnValueChanged -= UpdateKeys;
    }

    void UpdateKeys(float manaValue)
    {
        float quarterValue = _maxMana.Value / 4f;

        UpdateKeysList(_damageKeys, _damageColors, manaValue, quarterValue);
        UpdateKeysList(_healthKeys, _healthColors, manaValue, quarterValue);
    }

    void UpdateKeysList(List<GameObject> keys, List<Material> onMaterials, float manaValue, float quarterValue)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            // Calculate the quarter index for this key
            int quarterIndex = Mathf.FloorToInt(manaValue / quarterValue);

            // Check if the key should be turned on for the specific quarter
            bool shouldTurnOn = i <= quarterIndex && manaValue >= quarterValue * (i + 1);

            if (shouldTurnOn)
            {
                // Use the corresponding "on" material for this key
                if (i < onMaterials.Count)
                {
                    SetKeyMaterial(keys[i], onMaterials[i]);
                }
                else
                {
                    Debug.LogError("Invalid onMaterials list. Make sure it has enough materials for all keys.");
                }
            }
            else
            {
                SetKeyMaterial(keys[i], _offMaterial);
            }
        }
    }

    void SetKeyMaterial(GameObject key, Material material)
    {
        Renderer renderer = key.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Assuming the material has a color property
            renderer.material = material;
        }
        else
        {
            Debug.LogError("Key GameObject is missing a Renderer component.");
        }
    }
}
