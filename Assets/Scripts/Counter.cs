using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class Counter : MonoBehaviour
{
    [SerializeField] private CoinsSpawner _coinsSpawner;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _text.text = "0";
    }

    private void Update()
    {
        foreach (Coin coin in _coinsSpawner._coins)
        {
            if (coin._isTouched == true)
            {
                _text.text = (int.Parse(_text.text) + 1).ToString();
            }
        }
    }
}
