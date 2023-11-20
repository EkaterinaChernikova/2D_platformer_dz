using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Cat : MonoBehaviour
{
    private const string Trigger = "Licking";

    //private Coroutine _patrol;

    public Animator _animator { get; private set; }
    public SpriteRenderer _spriteRenderer { get; private set; }
    public WaitForSeconds _timer { get; private set; }
    public int _delay { get; private set; }
    public int _minimalDelay { get; private set; } = 2;
    public int _maximalDelay { get; private set; } = 4;
    public int _walkBorder { get; private set; } = 5;
    public float _speed { get; private set; } = 1.0f;
    public float _damage { get; private set; } = 10.0f;
    public float _force { get; private set; } = 1.0f;
    public bool _isMoveLeft { get; private set; } = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_patrol = StartCoroutine(PatrolCycle());
        //SetTimer();
    }

    //private void Update()
    //{
    //    transform.Translate(transform.right * _speed * Time.deltaTime * (_isMoveLeft ? -1 : 1));
    //
    //    if (transform.position.x < -_walkBorder || transform.position.x > _walkBorder)
    //    {
    //        _isMoveLeft = !_isMoveLeft;
    //        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth component))
    //    {
    //        component.TakeDamage(_damage);
    //        collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D);
    //        rigidbody2D.AddForce((Vector2.up + (_isMoveLeft ? Vector2.left : Vector2.right)) * _force, ForceMode2D.Impulse);
    //    }
    //}
    //
    //private void SetTimer()
    //{
    //    _delay = Random.Range(_minimalDelay, _maximalDelay);
    //    _timer = new WaitForSeconds(_delay);
    //}
    //
    //private IEnumerator PatrolCycle()
    //{
    //    while (true)
    //    {
    //        yield return _timer;
    //        SetTimer();
    //        _isMoveLeft = (Random.value > AverageValue);
    //        _spriteRenderer.flipX = _isMoveLeft;
    //    }
    //}
}
