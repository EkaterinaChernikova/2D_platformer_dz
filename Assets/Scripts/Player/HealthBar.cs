using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private const float MinValue = 0.0f;

    [SerializeField] private Slider _bar;
    [SerializeField] private PlayerHealth _playerHealth;

    private float _cheangeSpeed = 100.0f;
    private Coroutine _changeValueCoroutine;
    private Image _barImage;
    private Gradient _barGradient = new Gradient();
    private GradientColorKey[] _colorKeys;
    private GradientColorKey _greenZone = new GradientColorKey(Color.green, 1.0f);
    private GradientColorKey _yellowZone = new GradientColorKey(Color.yellow, 0.7f);
    private GradientColorKey _redZone = new GradientColorKey(Color.red, 0.3f);
    private GradientAlphaKey[] _alphaKeys;
    private GradientAlphaKey _minimalAlpha = new GradientAlphaKey(1.0f, 0.0f);
    private GradientAlphaKey _maximalAlpha = new GradientAlphaKey(1.0f, 1.0f);

    private void OnEnable()
    {
        _playerHealth.ValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _playerHealth.ValueChanged -= OnValueChanged;
    }

    private void Start()
    {
        _colorKeys = new GradientColorKey[] { _greenZone, _yellowZone, _redZone };
        _alphaKeys = new GradientAlphaKey[] { _minimalAlpha, _maximalAlpha };
        _barGradient.SetKeys(_colorKeys, _alphaKeys);
        _barImage = _bar.fillRect.GetComponent<Image>();
        _barImage.color = Color.green;
        _bar.maxValue = _playerHealth.GetMaxValue();
        _bar.minValue = MinValue;
        OnValueChanged();
    }

    private void OnValueChanged()
    {
        if (_changeValueCoroutine != null)
        {
            StopCoroutine(_changeValueCoroutine);
        }

        _changeValueCoroutine = StartCoroutine(ChangeValue());
    }

    IEnumerator ChangeValue()
    {
        while (_bar.value != _playerHealth.GetValue())
        {
            _barImage.color = _barGradient.Evaluate(_bar.value / _bar.maxValue);
            _bar.value = Mathf.MoveTowards(_bar.value, _playerHealth.GetValue(), _cheangeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}