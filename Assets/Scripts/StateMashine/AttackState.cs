using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private StateMachine _stateMachine;

    public AttackState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Run()
    {
        throw new System.NotImplementedException();
    }
}
