using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private const float AverageValue = 0.5f;

    private Detector _detector;
    private StateMachine _stateMachine;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private float _speed;
    private float _walkBorder;
    private float _minimalDelay = 1.0f;
    private float _maximalDelay = 5.0f;
    private float _delay;
    private float _timer;
    private bool _isMoveLeft;
    private bool _isPlayerDetected;
    private bool _isPlayerDead;

    public PatrolState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _detector = _stateMachine.gameObject.GetComponentInChildren<Detector>();
        _detector.gameObject.SetActive(false);
    }

    public void Enter()
    {
        _transform = _stateMachine._cat.transform;
        _spriteRenderer = _stateMachine._cat._spriteRenderer;
        _speed = _stateMachine._cat._speed;
        _walkBorder = _stateMachine._cat._walkBorder;
        _isMoveLeft = _stateMachine._cat._isMoveLeft;
        _detector.gameObject.SetActive(true);
        SetTimer();
    }

    public void Exit()
    {
        _detector.gameObject.SetActive(false);
        _stateMachine.SetConditions(_isPlayerDead, _isPlayerDetected);
        _stateMachine.ChangeState();
    }

    public void Run()
    {
        _transform.Translate(_transform.right * _speed * Time.deltaTime * (_isMoveLeft ? -1 : 1));

        if (_transform.position.x < -_walkBorder || _transform.position.x > _walkBorder)
        {
            _isMoveLeft = !_isMoveLeft;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        _timer += Time.deltaTime;

        if (_timer > _delay)
        {
            SetTimer();
            ChangeDirection();
        }

        if (_isPlayerDetected == true)
        {
            Exit();
        }
    }

    private void SetTimer()
    {
        _delay = Random.Range(_minimalDelay, _maximalDelay);
        _timer = 0.0f;
    }

    private void ChangeDirection()
    {
        _isMoveLeft = (Random.value > AverageValue);
        _spriteRenderer.flipX = _isMoveLeft;
    }

    public void SetPlayerDetected(bool isDetected)
    {
        _isPlayerDetected = isDetected;
    }
}