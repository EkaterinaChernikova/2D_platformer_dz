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

    private bool _isInAir = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_rigidbody2D.velocity.y > 0 || _rigidbody2D.velocity.y < 0)
        {
            _isInAir = true;
        }
        else
        {
            _isInAir = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isInAir == false)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * _speed * Time.deltaTime;
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * _speed * Time.deltaTime;
            _spriteRenderer.flipX = true;
        }
    }
}