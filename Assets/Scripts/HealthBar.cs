using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float MinValue = 0.0f;
    private const float MaxValue = 100.0f;

    [SerializeField] private Slider _bar;

    private Image _barImage;
    private float _cheangeSpeed = 100.0f;
    private float _targetValue;
    private int _redZone = 30;
    private int _yellowZone = 70;

    private void Start()
    {
        _barImage = _bar.fillRect.GetComponent<Image>();
        _bar.minValue = MinValue;
        _bar.maxValue = MaxValue;
        _bar.value = _bar.maxValue;
        _barImage.color = Color.green;
        _targetValue = _bar.value;
    }

    private void Update()
    {
        if (_bar.value > _yellowZone)
        {
            _barImage.color = Color.green;
        }
        else if (_bar.value > _redZone)
        {
            _barImage.color = Color.yellow;
        }
        else
        {
            _barImage.color = Color.red;
        }

        if (_targetValue != _bar.value)
        {
            ChangeValue();
        }
    }

    private void ChangeValue()
    {
        if (_targetValue < MinValue)
        {
            _targetValue = MinValue;
        }
        else if (_targetValue > MaxValue)
        {
            _targetValue = MaxValue;
        }

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
