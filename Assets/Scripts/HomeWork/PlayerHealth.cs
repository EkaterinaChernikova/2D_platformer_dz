using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _healthPoints = 100.0f;

    public void Die()
    {
        _playerAnimations.Die();
        _playerMovement.enabled = false;
    }

    public void TakeHeal(float value)
    {
        _healthBar.SetTargetValue(value);
    }

    public void TakeDamage(float value)
    {
        _healthBar.SetTargetValue(-value);
    }

    public float GetHealthPoints()
    {
        return _healthPoints;
    }
}
