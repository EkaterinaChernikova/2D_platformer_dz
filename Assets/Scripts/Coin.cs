using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Coin : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    public event Action<Coin> CoinTouched;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinTouched?.Invoke(this);
    }

    public void SetVisibility(bool isOn)
    {
        _spriteRenderer.enabled = isOn;
        _collider.enabled = isOn;
    }
}