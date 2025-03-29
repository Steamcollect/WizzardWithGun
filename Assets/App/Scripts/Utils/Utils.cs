using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

public static class Utils
{
    
    #region IENUMERABLE
    public static T GetRandom<T>(this IEnumerable<T> elems)
    {
        if (!elems.Any())
        {
            Debug.LogError("Try to get random elem from empty IEnumerable");
        }
        return elems.ElementAt(UnityEngine.Random.Range(0, elems.Count()));
    }
    #endregion

    #region COROUTINE

    public static void Delay(this MonoBehaviour hook, Action ev, YieldInstruction yieldInstruction)
    {
        IEnumerator DelayCoroutine()
        {
            yield return yieldInstruction;
            ev?.Invoke();
        }

        hook.StartCoroutine(DelayCoroutine());
    }
    
    public static void Delay(this MonoBehaviour hook, Action ev, float time)
    {
        IEnumerator DelayCoroutine()
        {
            yield return new WaitForSeconds(time);
            ev?.Invoke();
        }

        hook.StartCoroutine(DelayCoroutine());
    }
    
    public static void Delay(this MonoBehaviour hook, Action ev, IEnumerator coroutine)
    {
        IEnumerator DelayCoroutine()
        {
            yield return coroutine;
            ev?.Invoke();
        }

        hook.StartCoroutine(DelayCoroutine());
    }
    
    #endregion
    
    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}