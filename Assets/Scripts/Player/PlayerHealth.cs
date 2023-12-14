using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    private const float MinValue = 0.0f;

    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _healthPoints = 100.0f;

    private float _maxValue;
    private float _currentHealth;

    public event Action ValueChanged;

    private void Start()
    {
        _maxValue = _healthPoints;
        _currentHealth = _maxValue;
    }

    private void Die()
    {
        _playerAnimations.Die();
        _playerMovement.enabled = false;
    }

    private void SetHealthValue(float value)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, MinValue, _maxValue);

        if (_currentHealth == 0)
        {
            Die();
        }

        ValueChanged?.Invoke();
    }

    public void TakeHeal(float value)
    {
        SetHealthValue(value);
    }

    public void TakeDamage(float value)
    {
        SetHealthValue(-value);
    }

    public float GetValue()
    {
        return _currentHealth;
    }

    public float GetMaxValue()
    {
        return _healthPoints;
    }
}