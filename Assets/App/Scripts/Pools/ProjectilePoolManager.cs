using MVsToolkit.Pool;
using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] MVsPool<Projectile>[] projectiles;

    [Header("References")]
    [SerializeField] RSF_GetProjectile rsfGetProjetctile;

    private void OnEnable()
    {
        rsfGetProjetctile.Add(GetProjectile);
    }
    private void OnDisable()
    {
        rsfGetProjetctile.Remove(GetProjectile);
    }

    private void Start()
    {
        foreach (MVsPool<Projectile> pool in projectiles)
        {
            pool.Init(p => p.Initialize(pool));
        }
    }

    Projectile GetProjectile(ProjectileType type)
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