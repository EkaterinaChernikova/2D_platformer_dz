using UnityEngine;
using System;

public class AttackState : IState
{
    private Rigidbody2D _rigidbody2D;
    private Rigidbody2D _targetRigidbody2D;
    private StateMachine _stateObject;
    private CatAnimation _catAnimation;
    private CatMovement _catMovement;
    private Detector _detector;

    private Vector2 _jumpDirection;

    private float _damage = 5.0f;
    private float _punchForce = 2.0f;
    private bool _isJumpLeft;
    private bool _isPunched;
    private bool _isDetected;
    private bool _isTouched;
    private bool _isDead;
    public event Action onStateEnd;

    public AttackState(Detector detector, CatAnimation catAnimation, CatMovement catMovement)
    {
        _detector = detector;
        _catAnimation = catAnimation;
        _catMovement = catMovement;
        detector.onTargetDetected += SetDetection;
        detector.onTargetTouched += SetIsTouched;
        detector.onTargetDead += SetIsDead;
    }

    private void SetDetection(bool isDetected)
    {
        _isDetected = isDetected;
    }

    private void SetIsTouched(bool isTouched)
    {
        _isTouched = isTouched;
    }

    private void SetIsDead(bool isDead)
    {
        _isDead = isDead;
    }

    public void Enter()
    {

    }

    public void Run()
    {
        if (_catMovement.IsGrounded == true)
        {
            if (_isDetected == true)
            {
                InitJumpAttack();
            }
            else if (_isDetected == false)
            {
                Exit();
            }
        }
        else if (_catMovement.IsGrounded == false)
        {
            _catMovement.JumpProcessing();

            if (_isTouched == true && _isDead == false && _isPunched == false)
            {
                _isPunched = true;
                _detector.targetHealth.TakeDamage(_damage);
                _detector.playerRigidbody2D.AddForce((Vector2.up + _jumpDirection) * _punchForce, ForceMode2D.Impulse);
            }
        }
    }

    private void InitJumpAttack()
    {
        _isPunched = false;
        _isJumpLeft = _detector.transform.position.x > _detector.targetTransform.position.x ? true : false;
        _jumpDirection = _isJumpLeft ? Vector2.left : Vector2.right;
        _catAnimation.Flip(_isJumpLeft);
        _catMovement.Jump(_jumpDirection);
    }

    private void Exit()
    {
        onStateEnd?.Invoke();
    }
}