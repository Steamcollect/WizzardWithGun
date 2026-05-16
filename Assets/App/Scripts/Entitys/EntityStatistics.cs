using MVsToolkit.Attributes;
using System.Runtime.CompilerServices;
using UnityEngine;
public class EntityStatistics : MonoBehaviour
{
    [SerializeField] SSO_EntityData data;

    [Space]
    [ReadOnly] public int Health;
    [ReadOnly] public float MoveSpeed;
    [ReadOnly] public int Damage;

    [Space]
    [ReadOnly] public SSO_EntitySpawningWeapon SpawningWeapon;

    private void Awake()
    {
        Health = data.Health;
        MoveSpeed = data.MoveSpeed;
        Damage = data.Damage;

        SpawningWeapon = data.SpawningWeapon;
    }

    public SSO_EntityData GetData() => data;
}