using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cat))]

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, IState> _posibleStates;
    private IState _currentState;
    private bool _isPlayerDead;
    private bool _isPlayerDetected;

    public Cat _cat { get; private set; }

    private void Start()
    {
        InitStates();
        SetStateByDefault(GetState<PatrolState>());
        _cat = gameObject.GetComponent<Cat>();
    }

    private void InitStates()
    {
        _posibleStates = new Dictionary<Type, IState>();

        _posibleStates[typeof(IdleState)] = new IdleState(this);
        _posibleStates[typeof(AttackState)] = new AttackState(this);
        _posibleStates[typeof(PatrolState)] = new PatrolState(this);
    }

    private void SetState(IState nextState)
    {
        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }

    private void SetStateByDefault(IState defaultState)
    {
        SetState(defaultState);
    }

    private IState GetState<T>() where T : IState
    {
        return _posibleStates[typeof(T)];
    }

    private void Update()
    {
        _currentState?.Run();
    }

    public void ChangeState()
    {
        if (_isPlayerDead == true && _isPlayerDetected == true)
        {
            SetState(GetState<IdleState>());
        }
        else if (_isPlayerDead == false && _isPlayerDetected == true)
        {
            SetState(GetState<AttackState>());
        }
        else if (_isPlayerDead == false && _isPlayerDetected == false)
        {
            SetState(GetState<PatrolState>());
        }

        SetConditions(false, false);
    }

    public void SetConditions(bool isPlayerDead, bool isPlayerDetected)
    {
        _isPlayerDead = isPlayerDead;
        _isPlayerDetected = isPlayerDetected;
    }
}