using MVsToolkit.Pool;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] MVsPool<Projectile>[] projectiles;

    static ProjectilePoolManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        foreach (MVsPool<Projectile> pool in projectiles)
        {
            pool.Init(p => p.Initialize(pool));
        }
    }

    public static Projectile GetProjectile(ProjectileType type)
    {
        if (instance != null) return instance._GetProjectile(type);
        else return null;
    }

    Projectile _GetProjectile(ProjectileType type)
    {
        if (projectiles[(int)type].TryGet(out Projectile p))
        {
            return p;
        }
        else
        {
            Debug.LogError($"Impossible to find projectile of type {type} anymore");
            return null;
        }
    }
}