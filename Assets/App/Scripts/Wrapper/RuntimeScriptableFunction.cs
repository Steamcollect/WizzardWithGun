using System;
using UnityEngine;

public class RuntimeScriptableFunction<T> : ScriptableObject
{
    public event Func<T> Action;
    public T Call()
    {
        return Action != null ? Action.Invoke() : default;
    }

    public void AddListener(Func<T> func) => Action += func;
    public void RemoveListener(Func<T> func) => Action -= func;
}

public class RuntimeScriptableFunction<T,T1> : ScriptableObject
{
    public event Func<T1,T> Action;
    public T Call(T1 t1)
    {
        return Action != null ? Action.Invoke(t1) : default;
    }

    public void AddListener(Func<T1, T> func) => Action += func;
    public void RemoveListener(Func<T1, T> func) => Action -= func;
}
public class RuntimeScriptableFunction<T,T1,T2> : ScriptableObject
{
    public event Func<T1,T2,T> Action;
    public T Call(T1 t1, T2 t2)
    {
        return Action != null ? Action.Invoke(t1,t2) : default;
    }

    public void AddListener(Func<T1, T2, T> func) => Action += func;
    public void RemoveListener(Func<T1, T2, T> func) => Action -= func;
}