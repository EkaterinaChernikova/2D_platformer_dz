using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour
{
    private const string Trigger = "Licking";
    private const float AverageValue = 0.5f;

    private Coroutine _patrol;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _timer;
    private int _delay;
    private int _minimalDelay = 2;
    private int _maximalDelay = 4;
    private int _walkBorder = 5;
    private float _speed = 1.0f;
    private float _damage = 10.0f;
    private float _force = 1.0f;
    private bool _isMoveLeft = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _patrol = StartCoroutine(PatrolCycle());
        SetTimer();
    }

    private void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime * (_isMoveLeft ? -1 : 1));

        if (transform.position.x < -_walkBorder || transform.position.x > _walkBorder)
        {
            _isMoveLeft = !_isMoveLeft;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth component))
        {
            component.TakeDamage(_damage);
            collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D);
            rigidbody2D.AddForce((Vector2.up + (_isMoveLeft ? Vector2.left : Vector2.right)) * _force, ForceMode2D.Impulse);
        }
    }

    private void SetTimer()
    {
        _delay = Random.Range(_minimalDelay, _maximalDelay);
        _timer = new WaitForSeconds(_delay);
    }

    private IEnumerator PatrolCycle()
    {
        while (true)
        {
            yield return _timer;
            SetTimer();
            _isMoveLeft = (Random.value > AverageValue);
            _spriteRenderer.flipX = _isMoveLeft;
        }
    }
}
