using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinsSpawner _spawner; 

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spawner.SetTouchedCoin(this);
    }

    public void SetVisibility(bool isOn)
    {
        _spriteRenderer.enabled = isOn;
        _collider.enabled = isOn;
    }
}