using UnityEngine;

public class BuildShaderManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _timeToReduce = 2.0f;
    [SerializeField] private float _disolveIniVal = 1.0f;
    [SerializeField] private float _disolveFinalVal = 0.0f;

    private MeshRenderer _myMeshRenderer;
    private Material[] _meshMaterials;

    private float _currentDisolve;

    private bool isBuildingEffect = false;
    private float _elapsedTime = 0;

    private void Start()
    {
        _myMeshRenderer = GetComponent<MeshRenderer>();
        _meshMaterials = _myMeshRenderer.materials;
        ChangeMaterialVariable("_disolve", _disolveIniVal);
    }
    private void Update()
    {
        if (!isBuildingEffect) return;
        BuildEffectInUpdate();
    }

    public void StartBuildEffect()
    {
        isBuildingEffect = true;
        _elapsedTime = 0f;
    }
    private void ChangeMaterialVariable(string variableName, float value)
    {
        for (int i = 0; i < _myMeshRenderer.materials.Length; i++)
        {
            try
            {
            _meshMaterials[i].SetFloat(variableName, value);
            }
            catch
            {
                Debug.LogError("Can't change disolve value");
            }
        }
    }

    private void BuildEffectInUpdate()
    {
        if (_elapsedTime < _timeToReduce)
        {
            _currentDisolve = Mathf.Lerp(_disolveIniVal, _disolveFinalVal, _elapsedTime / _timeToReduce);
            ChangeMaterialVariable("_disolve", _currentDisolve);
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            _currentDisolve = _disolveFinalVal;
            ChangeMaterialVariable("_disolve", _currentDisolve);
            Destroy(this);
            isBuildingEffect = false;
        }
    }

}
