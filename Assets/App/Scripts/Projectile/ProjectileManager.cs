using MVsToolkit.Pool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] MVsPool<Projectile>[] projectiles;

    [Header("References")]
    [SerializeField] RSF_GetProjectile rsfGetBullet;

    private void OnEnable()
    {
        rsfGetBullet.Add(GetBullet);
    }
    private void OnDisable()
    {
        rsfGetBullet.Remove(GetBullet);
    }

    private void Start()
    {
        foreach (MVsPool<Projectile> pool in projectiles)
        {
            pool.Init(p => p.Initialize(pool));
        }
    }

    Projectile GetBullet(ProjectileType type)
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