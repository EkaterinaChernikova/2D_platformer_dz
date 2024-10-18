using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 25;
    [SerializeField] private float _strikeForce = 5.0f;
    [SerializeField] private float _attackDelay = 0.5f;

    private int _attackButton = 0;

    private Rigidbody2D _targetRigidbody2D;
    private float _timer = 0.0f;
    private float _attackDistance = 1.75f;
    private bool _isAttacked;
    private bool _isAttack;
    private bool _isInRange;
    private SpriteRenderer _spriteRenderer;
    private PlayerAnimations _animation;
    private Vector2 _playerPosition;
    private Vector2 _attackAreaOffsetPointA = new Vector2(0f, -0.7f);
    private Vector2 _attackAreaOffsetPointB = new Vector2(1.25f, 0.75f);
    private Vector2 _attackAreaPointA;
    private Vector2 _attackAreaPointB;
    private Vector2 _attackSideCorrection;
    private Collider2D[] _targets;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animation = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_attackButton) == true && _isAttack == false)
        {
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
            Strike();
            _isAttacked = true;
        }

        if (_timer >= _attackDelay)
        {
            _timer = 0.0f;
            _isAttack = false;
            _isAttacked = false;
        }
    }

    private void Strike()
    {
        _playerPosition = new Vector2(transform.position.x, transform.position.y);
        _attackSideCorrection = Vector2.up + Vector2.right * (_spriteRenderer.flipX ? -1 : 1);
        _attackAreaPointA = (_playerPosition + _attackAreaOffsetPointA) * _attackSideCorrection;
        _attackAreaPointB = (_playerPosition + _attackAreaOffsetPointB) * _attackSideCorrection;
        _targets = Physics2D.OverlapAreaAll(_attackAreaPointA, _attackAreaPointB);
        
        foreach(Collider2D target in _targets)
        {
            _targetRigidbody2D = target.GetComponent<Rigidbody2D>();

            if (_attackDistance >= Vector2.Distance(transform.position, target.transform.position))
            {
                _isInRange = true;
            }

            if (_targetRigidbody2D != null && _targetRigidbody2D.gameObject.name != "Player" && _isInRange == true)
            {
                _targetRigidbody2D.AddForce(Vector2.right * (_spriteRenderer.flipX ? -1 : 1) * _strikeForce, ForceMode2D.Impulse);
            }

            _isInRange = false;
        }

        _targets = null;
    }
}
