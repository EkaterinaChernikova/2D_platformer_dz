using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private const string Idle = "isIdle";

    private StateMachine _stateObject;

    public IdleState(StateMachine stateMachine)
    {
        _stateObject = stateMachine;
    }

    private void SendResults()
    {
        _stateObject.SetConditions(_stateObject.Detector.isDead, _stateObject.Detector.isDetected);
        _stateObject.ChangeState();
    }

    public void Enter()
    {
        _stateObject.Cat.SwitchAnimation(Idle);
    }

    public void Run()
    {
        if (_stateObject.Detector.isDead == false)
        {
            SendResults();
        }
    }
}