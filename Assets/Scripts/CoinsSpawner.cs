using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private WaitForSeconds _timer;
    private Coin _touchedCoin = null;
    private int _delay = 5;

    private void Awake()
    {
        _timer = new WaitForSeconds(_delay);
    }

    private void Update()
    {
        if (_touchedCoin != null)
        {
            _counter.AddScore();
            StartCoroutine(Respawn(_touchedCoin));
            _touchedCoin = null;
        }
    }

    private IEnumerator Respawn(Coin coin)
    {
        coin.SetVisibility(false);
        yield return _timer;
        coin.SetVisibility(true);
    }

    public void SetTouchedCoin(Coin coin)
    {
        _touchedCoin = coin;
    }
}
