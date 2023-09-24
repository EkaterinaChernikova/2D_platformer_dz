using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerAnimations : MonoBehaviour
{
    private const string Idle = "IsIdle";
    private const string Run = "IsRun";
    private const string Jump = "IsJump";
    private const string Fall = "IsFall";
    private const string Dead = "IsDead";

    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody2D;

    private bool _isDead = false;
    private bool _isInAir = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDead == true)
        {
            return;
        }

        if (_rigidbody2D.velocity.y > 0)
        {
            SwitchAnimation(Jump);
            _isInAir = true;
        }
        else if (_rigidbody2D.velocity.y < 0)
        {
            SwitchAnimation(Fall);
            _isInAir = true;
        }
        else
        {
            SwitchAnimation(Idle);
            _isInAir = false;
        }

        if (_isInAir == false)
        {
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                SwitchAnimation(Idle);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                SwitchAnimation(Run);
            }
        }
    }

    private void SwitchAnimation(string state)
    {
        _animator.SetBool(Idle, false);
        _animator.SetBool(Run, false);
        _animator.SetBool(Jump, false);
        _animator.SetBool(Fall, false);

        _animator.SetBool(state, true);
    }

    public void Die()
    {
        if (_isDead == false)
        {
            SwitchAnimation(Dead);
            _isDead = true;
        }
    }
}
