using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    private const float MinValue = 0.0f;

    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Rigidbody2D _rigidbody2D;
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

    private void ChangeHealthValue(float value)
    {
        if (_currentHealth == MinValue)
        {
            return;
        }

        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, MinValue, _maxValue);

        if (_currentHealth == MinValue)
        {
            Die();
        }

        ValueChanged?.Invoke();
    }

    public void TakeHeal(float value)
    {
        ChangeHealthValue(value);
    }

    public void TakeDamage(float value)
    {
        ChangeHealthValue(-value);
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