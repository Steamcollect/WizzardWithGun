using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]
    public Action onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke();
        _OnTriggerEnter(other);
    }
    protected abstract void _OnTriggerEnter(Collider other);
}