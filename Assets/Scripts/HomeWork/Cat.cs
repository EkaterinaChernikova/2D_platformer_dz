using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class Cat : MonoBehaviour
{
    private const string Idle = "isIdle";
    private const string Patrol = "isPatrol";
    private const string JumpStart = "isJumpStart";
    private const string JumpEnd = "isJumpEnd";

    private Animator _animator;

    public float speed { get; private set; } = 1.0f;
    public float damage { get; private set; } = 10.0f;
    public float force { get; private set; } = 1.0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchAnimation(string state)
    {
        _animator.SetBool(Idle, false);
        _animator.SetBool(Patrol, false);
        _animator.SetBool(JumpStart, false);
        _animator.SetBool(JumpEnd, false);

        _animator.SetBool(state, true);
    }
}