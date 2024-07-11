using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CatAnimation))]

public class CatMovement : MonoBehaviour
{
    private const float AverageValue = 0.5f;
    private const string JumpStart = "isJumpStart";
    private const string JumpEnd = "isJumpEnd";

    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _walkBorder = 5.0f;

    private Rigidbody2D _rigidbody2D;
    private CatAnimation _catAnimation;
    private float _horizontalJumpForce = 2.5f;
    private float _verticalJumpForce = 3.0f;
    private float _minimalForceCorrection = 0.9f;
    private float _maximalForceCorrection = 1.1f;
    private float _horizontalForce;
    private float _verticalForce;
    private bool _isMoveLeft;

    public bool IsGrounded { get; private set; } = true;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _catAnimation = GetComponent<CatAnimation>();
    }

    public void Move()
    {
        transform.Translate(_speed * Time.deltaTime * (_isMoveLeft ? -Vector2.right : Vector2.right));

        if (transform.position.x < -_walkBorder)
        {
            _isMoveLeft = false;
            _catAnimation.Flip(false);
        }
        else if (transform.position.x > _walkBorder)
        {
            _isMoveLeft = true;
            _catAnimation.Flip(true);
        }
    }

    public void Jump(Vector2 jumpDirection)
    {
        _verticalForce = Random.Range(_verticalJumpForce * _minimalForceCorrection, _verticalJumpForce * _maximalForceCorrection);
        _horizontalForce = Random.Range(_horizontalJumpForce * _minimalForceCorrection, _horizontalJumpForce * _maximalForceCorrection);
        _rigidbody2D.AddForce(Vector2.up * _verticalForce + jumpDirection * _horizontalForce, ForceMode2D.Impulse);
        IsGrounded = false;
    }

    public void JumpProcessing()
    {
        if (_rigidbody2D.velocity.y > 0)
        {
            _catAnimation.SwitchAnimation(JumpStart);
        }
        else if (_rigidbody2D.velocity.y < 0)
        {
            _catAnimation.SwitchAnimation(JumpEnd);
        }

        if (_rigidbody2D.velocity.y == 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    public void ChangeDirection()
    {
        _isMoveLeft = (Random.value > AverageValue);
        _catAnimation.Flip(_isMoveLeft);
    }
}
