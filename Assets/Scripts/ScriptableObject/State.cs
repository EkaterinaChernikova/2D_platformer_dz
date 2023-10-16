using UnityEngine;

public abstract class State : ScriptableObject
{
    public bool isFinished { get; protected set; }
    
    public virtual void Init() { }

    public abstract void Run();
}
