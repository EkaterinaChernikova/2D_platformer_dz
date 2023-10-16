using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const float MinValue = 0.0f;

    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _healthPoints = 100.0f;

    private float _maxValue;
    private float _currentHealth;

    private void Start()
    {
        _maxValue = _healthPoints;
        _currentHealth = _maxValue;
    }

    private void Update()
    {
        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _playerAnimations.Die();
        _playerMovement.enabled = false;
    }

    private void SetHealthValue(float value)
    {
        if (_currentHealth != 0)
        {
            _currentHealth += value;
            _currentHealth = Mathf.Clamp(_currentHealth, MinValue, _maxValue);
        }
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
