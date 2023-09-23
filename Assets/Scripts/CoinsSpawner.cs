using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    private WaitForSeconds _timer;
    private int _delay = 5;
    public List<Coin> _coins { get; private set; } = new List<Coin>();

    private void Awake()
    {
        _timer = new WaitForSeconds(_delay);

        for (int i = 0; i < transform.childCount; i++)
        {
            _coins.Add(transform.GetChild(i).GetComponent<Coin>());
        }
    }

    private void Update()
    {
        foreach (Coin coin in _coins)
        {
            if (coin._isTouched == true)
            {
                coin.IsTouched(false);
                StartCoroutine(Respawn(coin));
            }
        }
    }

    private IEnumerator Respawn(Coin coin)
    {
        coin.SetVisibility(false);
        yield return _timer;
        coin.SetVisibility(true);
    }
}
