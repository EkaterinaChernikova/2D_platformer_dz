using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class CatAnimation : MonoBehaviour
{
    private const string Idle = "isIdle";
    private const string Patrol = "isPatrol";
    private const string JumpStart = "isJumpStart";
    private const string JumpEnd = "isJumpEnd";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SwitchAnimation(string state)
    {
        _animator.SetBool(Idle, false);
        _animator.SetBool(Patrol, false);
        _animator.SetBool(JumpStart, false);
        _animator.SetBool(JumpEnd, false);
        _animator.SetBool(state, true);
    }
    public void Flip(bool isFlip)
    {
        _spriteRenderer.flipX = isFlip;
    }
}
