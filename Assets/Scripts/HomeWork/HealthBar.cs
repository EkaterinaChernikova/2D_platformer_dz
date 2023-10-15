using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float MinValue = 0.0f;

    [SerializeField] private Slider _bar;
    [SerializeField] private PlayerHealth _playerHealth;

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
        _barGradient.SetKeys(_colorKeys, _alphaKeys);
        _barImage = _bar.fillRect.GetComponent<Image>();
        _barImage.color = Color.green;
        _bar.maxValue = _playerHealth.GetMaxValue();
        _bar.minValue = MinValue;
    }

    private void Update()
    {
        if (_playerHealth.GetValue() != _bar.value)
        {
            ChangeValue();
        }
    }

    private void ChangeValue()
    {
        _bar.value = _playerHealth.GetValue();
        _barImage.color = _barGradient.Evaluate(_bar.value / _bar.maxValue);
    }
}