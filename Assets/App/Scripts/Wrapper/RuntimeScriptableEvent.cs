using System;
using UnityEngine;

public class RuntimeScriptableEvent : ScriptableObject
{
    public event Action Action;
    public void Call() => Action?.Invoke();

    public void AddListener(Action action) => Action += action;
    public void RemoveListener(Action action) => Action -= action;
}

public class RuntimeScriptableEvent<T> : ScriptableObject
{
    public event Action<T> Action;
    public void Call(T t) => Action?.Invoke(t);

    public void AddListener(Action<T> action) => Action += action;
    public void RemoveListener(Action<T> action) => Action -= action;
}

public class RuntimeScriptableEvent<T,T1> : ScriptableObject
{
    public event Action<T,T1> Action;
    public void Call(T t, T1 t1) => Action?.Invoke(t,t1);

    public void AddListener(Action<T, T1> action) => Action += action;
    public void RemoveListener(Action<T, T1> action) => Action -= action;
}