using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
    private const string Idle = "IsIdle";
    private const string Run = "IsRun";
    private const string Jump = "IsJump";
    private const string Fall = "IsFall";
    private const string Dead = "IsDead";

    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _jumpForce = 1.0f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private bool _isDead = false;
    private bool _isInAir = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(_isDead == true)
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

            if (Input.GetKey(KeyCode.D))
            {
                SwitchAnimation(Run);
            }

            if (Input.GetKey(KeyCode.A))
            {
                SwitchAnimation(Run);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * _speed * Time.deltaTime;
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * _speed * Time.deltaTime;
            _spriteRenderer.flipX = true;
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
