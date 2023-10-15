using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const float MinValue = 0.0f;

    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _healthPoints = 100.0f;

    private float _maxValue;
    private float _currentHealth;
    private float _cheangeSpeed = 100.0f;
    private float _targetValue;

    private void Start()
    {
        _maxValue = _healthPoints;
        _currentHealth = _maxValue;
        _targetValue = _currentHealth;
    }

    private void Update()
    {
        if (_targetValue != _currentHealth)
        {
            ChangeValue();
        }
        else if (_targetValue == 0)
        {
            Die();
        }
    }

    private void ChangeValue()
    {
        _targetValue = Mathf.Clamp(_targetValue, MinValue, _maxValue);
        _currentHealth = Mathf.MoveTowards(_currentHealth, _targetValue, _cheangeSpeed * Time.deltaTime);
    }

    private void Die()
    {
        _playerAnimations.Die();
        _playerMovement.enabled = false;
    }

    private void SetTargetValue(float value)
    {
        _targetValue += value;
    }

    public void TakeHeal(float value)
    {
        SetTargetValue(value);
    }

    public void TakeDamage(float value)
    {
        SetTargetValue(-value);
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
