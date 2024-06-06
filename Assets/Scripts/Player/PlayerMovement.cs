using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _jumpForce = 1.0f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private KeyCode _gumpButton = KeyCode.Space;
    private KeyCode _moveLeftButton = KeyCode.A;
    private KeyCode _moveRightButton = KeyCode.D;
    private float _direction;
    private float _speedDelta;
    private bool _isInAir = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_gumpButton) && _isInAir == false)
        {
            _rigidbody2D.velocity += Vector2.up * _jumpForce;
        }

        _direction = Input.GetAxisRaw("Horizontal");

        if (_direction != 0)
        {
            _speedDelta = _speed - Mathf.Abs(_rigidbody2D.velocity.x);
            _rigidbody2D.velocity += Vector2.right * _speedDelta * _direction;

            if (_direction < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_direction > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }

        if (Input.GetKeyUp(_moveLeftButton) || Input.GetKeyUp(_moveRightButton) || 
            Input.GetKey(_moveLeftButton) && Input.GetKey(_moveRightButton))
        {
            _rigidbody2D.velocity *= Vector2.up;
        }

        if (_rigidbody2D.velocity.y > 0 || _rigidbody2D.velocity.y < 0)
        {
            _isInAir = true;
        }
        else
        {
            _isInAir = false;
        }
    }
}