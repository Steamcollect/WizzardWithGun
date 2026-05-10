using MVsToolkit.Pool;
using UnityEngine;

public class WeaponPoolManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] MVsPool<Weapon>[] weapons;

    [Header("References")]
    [SerializeField] RSF_GetWeapon rsfGetWeapon;

    private void OnEnable()
    {
        rsfGetWeapon.Add(GetWeapon);
    }
    private void OnDisable()
    {
        rsfGetWeapon.Remove(GetWeapon);
    }

    private void Start()
    {
        foreach (MVsPool<Weapon> pool in weapons)
        {
            pool.Init(p => p.Initialize(pool));
        }
    }

    Weapon GetWeapon(WeaponType type)
    {
        if (weapons[(int)type].TryGet(out Weapon w))
        {
            return w;
        }
        else
        {
            Debug.LogError($"Impossible to find weapon of type {type} anymore");
            return null;
        }
    }
}