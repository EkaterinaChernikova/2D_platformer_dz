using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private const string JumpStart = "isJumpStart";
    private const string JumpEnd = "isJumpEnd";

    private Rigidbody2D _rigidbody2D;
    private Rigidbody2D _targetRigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private StateMachine _stateObject;

    private Vector2 _jumpDirection;

    private float _damage;
    private float _punchForce = 2.0f;
    private float _horizontalJumpForce = 2.5f;
    private float _verticalJumpForce = 3.0f;
    private float _minimalForceCorrection = 0.9f;
    private float _maximalForceCorrection = 1.1f;
    private float _horizontalForce;
    private float _verticalForce;

    private bool _isGrounded = true;
    private bool _isJumpLeft;
    private bool _isPunched;

    public AttackState(StateMachine stateMachine)
    {
        _stateObject = stateMachine;
        _spriteRenderer = _stateObject.GetComponent<SpriteRenderer>();
        _rigidbody2D = _stateObject.GetComponent<Rigidbody2D>();
    }

    private void SetAnimation(string jumpState)
    {
        _stateObject.cat.SwitchAnimation(jumpState);
    }

    private void SendResults()
    {
        _stateObject.SetConditions(_stateObject.detector.isDead, _stateObject.detector.isDetected);
        _stateObject.ChangeState();
    }

    public void Enter()
    {
        _damage = _stateObject.cat.damage;
    }

    public void Run()
    {
        if (_isGrounded == true)
        {
            if (_stateObject.detector.isDetected == true)
            {
                _isPunched = false;
                _isGrounded = false;
                _isJumpLeft = _stateObject.cat.transform.position.x > _stateObject.detector.GetTargetPosition().x ? true : false;
                _jumpDirection = _isJumpLeft ? Vector2.left : Vector2.right;
                _spriteRenderer.flipX = _isJumpLeft;
                _verticalForce = Random.Range(_verticalJumpForce * _minimalForceCorrection, _verticalJumpForce * _maximalForceCorrection);
                _horizontalForce = Random.Range(_horizontalJumpForce * _minimalForceCorrection, _horizontalJumpForce * _maximalForceCorrection);
                _rigidbody2D.AddForce(Vector2.up * _verticalForce + _jumpDirection * _horizontalForce, ForceMode2D.Impulse);
            }
            else if (_stateObject.detector.isDetected == false)
            {
                SendResults();
            }
        }
        else if (_isGrounded == false)
        {
            if (_rigidbody2D.velocity.y > 0)
            {
                SetAnimation(JumpStart);
            }
            else if (_rigidbody2D.velocity.y < 0)
            {
                SetAnimation(JumpEnd);
            }
            else if (_rigidbody2D.velocity.y == 0)
            {
                _isGrounded = true;
            }

            if (_stateObject.detector.isTouched == true && 
                _stateObject.detector.isDead == false &&
                _isPunched == false)
            {
                _isPunched = true;
                _stateObject.detector.GetTargetHealth().TakeDamage(_damage);
                _stateObject.detector.GetTargetRigidbody().AddForce((Vector2.up + _jumpDirection) * _punchForce, ForceMode2D.Impulse);
            }
            else if (_stateObject.detector.isDead == true)
            {
                SendResults();
            }
        }
    }
}