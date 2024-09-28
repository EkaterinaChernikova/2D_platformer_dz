using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimations))]

public class PlayerController : MonoBehaviour
{
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    private PlayerMovement _playerMovement;
    private PlayerAnimations _playerAnimations;

    private KeyCode _jumpButton = KeyCode.Space;
    private KeyCode _moveLeftButton = KeyCode.A;
    private KeyCode _moveRightButton = KeyCode.D;
    private KeyCode _attackButton = KeyCode.F;

    private void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (Input.GetKey(_moveLeftButton) || Input.GetKey(_moveRightButton))
        {

        }
    }
}
