using UnityEngine;

public class PatrolState : IState
{
    private const float AverageValue = 0.5f;
    private const string Patrol = "isPatrol";

    private StateMachine _stateObject;
    private SpriteRenderer _spriteRenderer;

    private float _walkBorder = 5.0f;
    private float _minimalDelay = 1.0f;
    private float _maximalDelay = 5.0f;
    private float _delay;
    private float _timer;
    private float _speed;
    private bool _isMoveLeft;

    public PatrolState(StateMachine stateMachine)
    {
        _stateObject = stateMachine;
        _spriteRenderer = _stateObject.GetComponent<SpriteRenderer>();
    }

    private void SetAnimation()
    {
        _stateObject.Cat.SwitchAnimation(Patrol);
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

    private void SendResults()
    {
        _stateObject.SetConditions(_stateObject.Detector.isDead, _stateObject.Detector.isDetected);
        _stateObject.ChangeState();
    }

    public void Enter()
    {
        _speed = _stateObject.Cat.speed;
        SetTimer();
        SetAnimation();
        ChangeDirection();
    }

    public void Run()
    {
        _stateObject.transform.Translate(Vector3.right * _speed * Time.deltaTime * (_isMoveLeft ? -1 : 1));

        if (_stateObject.transform.position.x < -_walkBorder)
        {
            _isMoveLeft = false;
            _spriteRenderer.flipX = false;
        }
        else if (_stateObject.transform.position.x > _walkBorder)
        {
            _isMoveLeft = true;
            _spriteRenderer.flipX = true;
        }

        _timer += Time.deltaTime;

        if (_timer > _delay)
        {
            SetTimer();
            ChangeDirection();
        }

        if (_stateObject.Detector.isDetected == true)
        {
            SendResults();
        }
    }
}