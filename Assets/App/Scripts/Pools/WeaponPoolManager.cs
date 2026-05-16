using MVsToolkit.Pool;
using UnityEngine;

public class WeaponPoolManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] MVsPool<Weapon>[] weapons;

    static WeaponPoolManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        foreach (MVsPool<Weapon> pool in weapons)
        {
            pool.Init(p => p.Initialize(pool));
        }
    }

    public static Weapon GetWeapon(WeaponType type)
    {
        if(instance != null) return instance._GetWeapon(type);
        else return null;
    }

    Weapon _GetWeapon(WeaponType type)
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