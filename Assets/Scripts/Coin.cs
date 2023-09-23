using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Coin : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    public bool _isTouched { get; private set; } = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isTouched = true;
        //SetVisibility(false);
    }

    public void SetVisibility(bool isOn)
    {
        _spriteRenderer.enabled = isOn;
        _collider.enabled = isOn;
    }

    public void IsTouched(bool isTrue)
    {
        _isTouched = isTrue;
    }
}