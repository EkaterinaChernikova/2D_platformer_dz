using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class Counter : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _text.text = "0";
    }

    public void AddScore()
    {
        _text.text = (int.Parse(_text.text) + 1).ToString();
    }
}