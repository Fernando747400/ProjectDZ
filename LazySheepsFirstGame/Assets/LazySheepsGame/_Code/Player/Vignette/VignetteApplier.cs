using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteApplier : MonoBehaviour
{
    public float intensity = 0.75f;
    public float duration = 0.5f;
    public Volume volume = null;

    private Vignette _vignette = null;
    private Vector3 lastPosition;

    private void Awake()
    {
        if (volume.profile.TryGet(out Vignette vignette)) this._vignette = vignette;
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition != lastPosition)
        {
            Fadein();
        }
        else
        {
            Fadeout();
        }

        lastPosition = currentPosition;
    }

    public void Fadein()
    {
        StartCoroutine(Fade(0, intensity));
    }

    public void Fadeout()
    {
        StartCoroutine(Fade(intensity, 0));
    }

    private IEnumerator Fade(float startValue, float endValue)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float blend = elapsedTime / duration;
            elapsedTime += Time.deltaTime;

            float intensity = Mathf.Lerp(startValue, endValue, blend);
            ApplyValue(intensity);
            yield return null;
        }
    }

    private void ApplyValue(float value)
    {
        _vignette.intensity.Override(value);
    }
}