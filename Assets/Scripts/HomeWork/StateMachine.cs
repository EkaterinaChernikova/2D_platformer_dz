using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cat))]
[RequireComponent(typeof(Detector))]
[RequireComponent(typeof(CatAnimation))]
[RequireComponent(typeof(CatMovement))]

public class StateMachine : MonoBehaviour
{
    private IState _currentState;
    private Dictionary<Type, IState> _posibleStates;
    private CatAnimation _catAnimation;
    private CatMovement _catMovement;
    private bool _isPlayerDead;
    private bool _isPlayerDetected;

    private Detector _detector;
    public Cat Cat { get; private set; }

    private void OnDisable()
    {
        _detector.onTargetDetected -= SetIsPlayerDetected;
        _detector.onTargetDead -= SetIsPlayerDead;

        foreach (IState state in _posibleStates.Values)
        {
            state.onStateEnd -= ChangeState;
        }
    }

    private void Start()
    {
        Cat = gameObject.GetComponent<Cat>();
        _detector = GetComponent<Detector>();
        _catAnimation = GetComponent<CatAnimation>();
        _catMovement = GetComponent<CatMovement>();
        InitStates();
        SetStateByDefault(GetState<PatrolState>());
        SetActions();
    }

    private void InitStates()
    {
        _posibleStates = new Dictionary<Type, IState>();

        _posibleStates[typeof(IdleState)] = new IdleState(_detector, _catAnimation);
        _posibleStates[typeof(AttackState)] = new AttackState(_detector, _catAnimation, _catMovement);
        _posibleStates[typeof(PatrolState)] = new PatrolState(_detector, _catAnimation, _catMovement);
    }

    private void SetActions()
    {
        _detector.onTargetDetected += SetIsPlayerDetected;
        _detector.onTargetDead += SetIsPlayerDead;

        foreach (IState state in _posibleStates.Values)
        {
            state.onStateEnd += ChangeState;
        }
    }

    private void SetState(IState nextState)
    {
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

    private void ChangeState()
    {
        if (_isPlayerDead == true && _isPlayerDetected == true)
        {
            SetState(GetState<IdleState>());
        }
        else if (_isPlayerDead == false && _isPlayerDetected == true)
        {
            SetState(GetState<AttackState>());
        }
        else if (_isPlayerDetected == false)
        {
            SetState(GetState<PatrolState>());
        }
    }

    private void SetIsPlayerDetected(bool isDetected)
    {
        _isPlayerDetected = isDetected;
    }

    private void SetIsPlayerDead(bool isDead)
    {
        _isPlayerDead = isDead;
    }
}