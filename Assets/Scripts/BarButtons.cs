using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarButtons : MonoBehaviour
{
    [SerializeField] private HealthBar _bar;

    private float _changeValue = 10.0f;

    public void HealButton()
    {
        _bar.SetTargetValue(_changeValue);
    }

    public void DamageButton()
    {
        _bar.SetTargetValue(-_changeValue);
    }
}
