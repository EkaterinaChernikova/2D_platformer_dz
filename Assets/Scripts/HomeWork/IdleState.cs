using System;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private const string Idle = "isIdle";

    private Detector _detector;
    private CatAnimation _catAnimation;
    private bool _isTargetDead;
    public event Action onStateEnd;

    public IdleState(Detector detector, CatAnimation catAnimation)
    {
        _detector = detector;
        _catAnimation = catAnimation;
        _detector.onTargetDetected += SetIsDead;
    }

    private void SetIsDead(bool isDead)
    {
        _isTargetDead = isDead;
    }

    private void Exit()
    {
        onStateEnd?.Invoke();
    }

    public void Enter()
    {
        _catAnimation.SwitchAnimation(Idle);
    }

    public void Run()
    {
        if (_isTargetDead == false)
        {
            Exit();
        }
    }
}