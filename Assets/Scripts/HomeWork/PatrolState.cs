using UnityEngine;
using System;

public class PatrolState : IState
{
    private const float AverageValue = 0.5f;
    private const string Patrol = "isPatrol";

    private Detector _detector;
    private CatMovement _catMovement;
    private CatAnimation _catAnimation;

    private float _minimalDelay = 1.0f;
    private float _maximalDelay = 5.0f;
    private float _delay;
    private float _timer;
    private bool _isMoveLeft;
    private bool _isTargetDetected;

    public event Action onStateEnd;

    public PatrolState(Detector detector, CatAnimation catAnimation, CatMovement catMovement)
    {
        _detector = detector;
        _catMovement = catMovement;
        _catAnimation = catAnimation;
        _detector.onTargetDetected += SetDetection;
    }

    private void SetTimer()
    {
        _delay = UnityEngine.Random.Range(_minimalDelay, _maximalDelay);
        _timer = 0.0f;
    }

    public void Enter()
    {
        SetTimer();
        _catAnimation.SwitchAnimation(Patrol);
    }

    public void Run()
    {
        _catMovement.Move();
        _timer += Time.deltaTime;

        if (_timer > _delay)
        {
            SetTimer();
            _catMovement.ChangeDirection();
        }

        if (_isTargetDetected == true)
        {
            Exit();
        }
    }

    private void SetDetection(bool isDetected)
    {
        _isTargetDetected = isDetected;
    }

    private void Exit()
    {
        onStateEnd?.Invoke();
    }
}