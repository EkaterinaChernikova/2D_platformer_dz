using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAnimations : MonoBehaviour
{
    private const string Idle = "IsIdle";
    private const string Run = "IsRun";
    private const string Jump = "IsJump";
    private const string Fall = "IsFall";
    private const string Dead = "IsDead";
    private const string Attack = "IsAttack";

    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private float _colliderOffsetX = 0.4f;
    private float _colliderOffsetY = -0.5f;
    private float _colliderSizeX = 0.9f;
    private float _colliderSizeY = 0.1f;

    private Vector2 _colliderOffset;
    private Vector2 _colliderSize;

    private bool _isDead = false;
    private bool _isInAir = false;
    private bool _isInAttack = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ChangeCollider()
    {
        if (_spriteRenderer.flipX == true)
        {
            _colliderOffsetX *= -1;
        }

        _colliderOffset = new Vector2(_colliderOffsetX, _colliderOffsetY);
        _colliderSize = new Vector2(_colliderSizeX, _colliderSizeY);

        _boxCollider2D.offset = _colliderOffset;
        _boxCollider2D.size = _colliderSize;
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

        if (_isInAir == false && _isInAttack == false)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                SwitchAnimation(Idle);
            }
            else
            {
                SwitchAnimation(Run);
            }
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
        {
            _isInAttack = true;
        }
        else
        {
            _isInAttack = false;
        }
    }

    private void SwitchAnimation(string state)
    {
        _animator.SetBool(Idle, false);
        _animator.SetBool(Run, false);
        _animator.SetBool(Jump, false);
        _animator.SetBool(Fall, false);
        _animator.SetBool(Attack, false);

        _animator.SetBool(state, true);
    }

    public void Die()
    {
        if (_isDead == false)
        {
            SwitchAnimation(Dead);
            _isDead = true;
            ChangeCollider();
        }
    }

    public void AnimateAttack()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") == false &&
            _isInAttack == false)
        {
            SwitchAnimation(Attack);
        }
    }
}