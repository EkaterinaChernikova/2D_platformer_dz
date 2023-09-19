using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Coin : MonoBehaviour
{
    [SerializeField] private Text _textMesh;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private WaitForSeconds _timer;
    private int _delay = 5;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _timer = new WaitForSeconds(_delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _textMesh.text = (int.Parse(_textMesh.text) + 1).ToString();
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        StartCoroutine(RespawnCoin());
    }

    private IEnumerator RespawnCoin()
    {
        yield return _timer;
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
}
