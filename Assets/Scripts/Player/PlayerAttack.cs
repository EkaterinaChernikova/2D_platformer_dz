using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 25;
    [SerializeField] private float _strikeForce = 5.0f;
    [SerializeField] private float _attackDelay = 0.5f;
    [SerializeField] private BoxCollider2D _attackCollider2D;

    private int _attackButton = 0;

    private Rigidbody2D _targetRigidbody2D;
    private float _timer = 0.0f;
    private bool _isAttacked;
    private bool _isAttack;
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimations _animation;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animation = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_attackButton) == true)
        {
            Debug.Log("Attack");
            _isAttack = true;
        }

        if (_isAttack == true)
        {
            MakeAttack();
        }
    }

    private void MakeAttack()
    {
        _timer += Time.deltaTime;

        if (_isAttacked == false)
        {
            _animation.AnimateAttack();
            _targetRigidbody2D?.AddForce(_strikeForce * Vector2.right * (_spriteRenderer.flipX ? -1 : 1));
            _isAttacked = true;
        }

        if (_timer >= _attackDelay)
        {
            _timer = 0.0f;
            _isAttack = false;
            _isAttacked = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D))
        {
            _targetRigidbody2D = rigidbody2D;
        }
    }
}
