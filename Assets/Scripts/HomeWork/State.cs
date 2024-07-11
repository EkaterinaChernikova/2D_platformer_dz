using System;
using UnityEngine;

public interface IState
{
    event Action onStateEnd;

    void Enter();

    void Run();
}