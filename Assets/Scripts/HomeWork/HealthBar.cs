using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float MinValue = 0.0f;
    private const float MaxValue = 100.0f;

    [SerializeField] private Slider _bar;

    private float _cheangeSpeed = 100.0f;
    private float _targetValue;
    private Image _barImage;
    private Gradient _barGradient = new Gradient();
    private GradientColorKey[] _colorKeys;
    private GradientColorKey _greenZone = new GradientColorKey(Color.green, 1.0f);
    private GradientColorKey _yellowZone = new GradientColorKey(Color.yellow, 0.7f);
    private GradientColorKey _redZone = new GradientColorKey(Color.red, 0.3f);
    private GradientAlphaKey[] _alphaKeys;
    private GradientAlphaKey _minimalAlpha = new GradientAlphaKey(1.0f, 0.0f);
    private GradientAlphaKey _maximalAlpha = new GradientAlphaKey(1.0f, 1.0f);

    private void Start()
    {
        _colorKeys = new GradientColorKey[] { _greenZone, _yellowZone, _redZone };
        _alphaKeys = new GradientAlphaKey[] { _minimalAlpha, _maximalAlpha };
        _barImage = _bar.fillRect.GetComponent<Image>();
        _bar.minValue = MinValue;
        _bar.maxValue = MaxValue;
        _bar.value = _bar.maxValue;
        _targetValue = _bar.value;
        _barGradient.SetKeys(_colorKeys, _alphaKeys);
    }

    private void Update()
    {
        _barImage.color = _barGradient.Evaluate(_bar.value / MaxValue);

        if (_targetValue != _bar.value)
        {
            ChangeValue();
        }
    }

    private void ChangeValue()
    {
        _targetValue = Mathf.Clamp(_targetValue, MinValue, MaxValue);

        _bar.value = Mathf.MoveTowards(_bar.value, _targetValue, _cheangeSpeed * Time.deltaTime);
    }

    public void SetTargetValue(float value)
    {
        _targetValue += value;
    }

    public float GetCurrentValue()
    {
        return _bar.value;
    }
}
