using System.Collections;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Counter _counter;

    private WaitForSeconds _timer;
    private Coin[] _coins;
    private int _delay = 5;

    private void Awake()
    {
        _timer = new WaitForSeconds(_delay);
        _coins = new Coin[transform.childCount];
        _coins = GetComponentsInChildren<Coin>();
    }

    private void OnEnable()
    {
        foreach (Coin coin in _coins)
        {
            coin.CoinTouched += OnCoinTouch; 
        }
    }

    private void OnDisable()
    {
        foreach (Coin coin in _coins)
        {
            coin.CoinTouched -= OnCoinTouch;
        }
    }

    private IEnumerator Respawn(Coin coin)
    {
        coin.SetVisibility(false);
        yield return _timer;
        coin.SetVisibility(true);
    }

    public void OnCoinTouch(Coin coin)
    {
        _counter.AddScore();
        StartCoroutine(Respawn(coin));
    }
}
